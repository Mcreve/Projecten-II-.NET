using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.ViewModels;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private ILearningUtilityRepository learningUtilityRepository;
        private IUserRepository userRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="learningUtilityRepository"></param>
        /// <param name="userRepository"></param>
        public WishlistController(ILearningUtilityRepository learningUtilityRepository, IUserRepository userRepository)
        {
            this.learningUtilityRepository = learningUtilityRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Basic index screen, which generates 2 partialviews.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult Index(User user)
        {
            Wishlist wishlist = user.Wishlist;
            if (wishlist == null||wishlist.NumberOfItems == 0)
                return View("EmptyWishlist");
            IEnumerable<WishlistViewModel> wishlistViewModels =
                wishlist.LearningUtilities.Select(learningUtility => 
                    new WishlistViewModel(GetLearningUtility(learningUtility.Id)))
                    .ToList();
            if (Request.IsAjaxRequest())
                return PartialView("_WishlistItemsPartial", wishlistViewModels);
            return View(wishlistViewModels);
        }
        /// <summary>
        /// Basic index screen, which generates 2 partialviews. [POST]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="wishlistViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(User user, WishlistViewModel wishlistViewModel)
        {
            Wishlist wishlist = user.Wishlist;
            foreach (var learningUtilitye in wishlist.LearningUtilities)
            {
                LearningUtility learningUtility = GetLearningUtility(learningUtilitye.Id);
                learningUtility.DateWanted = wishlistViewModel.Date;
            }
            IEnumerable<WishlistViewModel> wishlistViewModels =
                wishlist.LearningUtilities.Select(learningUtility =>
                    new WishlistViewModel(GetLearningUtility(learningUtility.Id))).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_WishlistItemsPartial", wishlistViewModels);
            }
            return View(wishlistViewModels);
        }
        /// <summary>
        /// Adds an item to the wishlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult Add(int id, User user)
        {
            if(user.Wishlist == null)
            {
                user.Wishlist = new Wishlist();
                userRepository.SaveChanges();
            }
            LearningUtility item = GetLearningUtility(id);
            if (item != null)
            {
                try
                {
                    user.Wishlist.AddItem(item);
                    TempData["info"] = item.Name + " werd toegevoegd aan uw verlanglijst.";
                    learningUtilityRepository.SaveChanges();
                }
                catch (InvalidOperationException e)
                {
                    TempData["error"] = e.Message;
                }
            }
            return RedirectToAction("Index", "Catalog");
        }

        /// <summary>
        /// Removes the item from the Wishlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult Remove(int id, User user)
        {
            Wishlist wishlist = user.Wishlist;
            LearningUtility item = GetLearningUtility(id);
            if (item != null)
            {
                try
                {
                    wishlist.RemoveItem(item);
                    TempData["info"] = item.Name + " werd van uw verlanglijstje verwijderd";
                    userRepository.SaveChanges();
                }
                catch (InvalidOperationException e)
                {
                    TempData["error"] = e.Message;
                }
            }
            if (Request.IsAjaxRequest())
            {
                IEnumerable<WishlistViewModel> wishlistViewModels =
                    wishlist.LearningUtilities.Select(
                        learningUtility => new WishlistViewModel(GetLearningUtility(learningUtility.Id)))
                        .ToList();
                return PartialView("_WishlistItemsPartial", wishlistViewModels);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Detailscreen of the Learning Utility
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ActionResult Details(int? id, DateTime date)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            LearningUtility learningUtility = learningUtilityRepository.FindBy((int)id);
            if (learningUtility == null)
            {
                return HttpNotFound();
            }

            LearningUtilityViewModel learningUtilityViewModel = new LearningUtilityViewModel(learningUtility);
            return View(learningUtilityViewModel);
        }

        /// <summary>
        /// Returns the learningUtility
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private LearningUtility GetLearningUtility(int id)
        {
            return learningUtilityRepository.FindBy(id);
        }
    }
}