using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.ViewModels;

namespace DidactischeLeermiddelen.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private ILearningUtilityRepository learningUtilityRepository;

        public WishlistController(ILearningUtilityRepository learningUtilityRepository)
        {
            this.learningUtilityRepository = learningUtilityRepository;
        }
        
        public ActionResult Index(Wishlist wishlist)
        {
            if (wishlist.NumberOfItems == 0)
                return View("EmptyWishlist");
            IEnumerable<WishlistViewModel> wishlistViewModels =
                wishlist.LearningUtilities.Select(learningUtility => 
                    new WishlistViewModel(GetLearningUtility(learningUtility.Id)))
                    .ToList();
            if (Request.IsAjaxRequest())
                return PartialView("_WishlistItemsPartial", wishlistViewModels);
            return View(wishlistViewModels);
        }

        [HttpPost]
        public ActionResult Index(Wishlist wishlist, WishlistViewModel wishlistViewModel)
        {
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

        public ActionResult Add(int id, Wishlist wishlist)
        {
            LearningUtility item = GetLearningUtility(id);
            if (item != null)
            {
                try
                {
                    wishlist.AddItem(item);
                    TempData["info"] = item.Name + " werd toegevoegd aan uw verlanglijst.";
                }
                catch (InvalidOperationException e)
                {
                    TempData["error"] = e.Message;
                }
            }
            return RedirectToAction("Index", "Catalog");
        }

        public ActionResult Remove(int id, Wishlist wishlist)
        {
            LearningUtility item = GetLearningUtility(id);
            if (item != null)
            {
                try
                {
                    wishlist.RemoveItem(item);
                    TempData["info"] = item.Name + " werd van uw verlanglijstje verwijderd";
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

        private LearningUtility GetLearningUtility(int id)
        {
            return learningUtilityRepository.FindBy(id);
        }
    }
}