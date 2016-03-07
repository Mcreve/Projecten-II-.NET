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
        private ILearningUtilityDetailsRepository learningUtilityDetailsRepository;

        public WishlistController(ILearningUtilityDetailsRepository learningUtilityDetailsRepository)
        {
            this.learningUtilityDetailsRepository = learningUtilityDetailsRepository;
        }
        
        public ActionResult Index(Wishlist wishlist)
        {
            if (wishlist.NumberOfItems == 0)
                return View("EmptyWishlist");
            IEnumerable<WishlistViewModel> wishlistViewModels =
                wishlist.LearningUtilities.Select(learningUtilityDetails => 
                    new WishlistViewModel(GetLearningUtilityDetails(learningUtilityDetails.Id)))
                    .ToList();
            return View(wishlistViewModels);
        }

        [HttpPost]
        public ActionResult Index(Wishlist wishlist, WishlistViewModel wishlistViewModel)
        {
            foreach (var learningUtilityDetailse in wishlist.LearningUtilities)
            {
                LearningUtilityDetails learningUtilityDetails = GetLearningUtilityDetails(learningUtilityDetailse.Id);
                learningUtilityDetails.DateWanted = wishlistViewModel.Date;
            }
            IEnumerable<WishlistViewModel> wishlistViewModels =
                wishlist.LearningUtilities.Select(learningUtilityDetails =>
                    new WishlistViewModel(GetLearningUtilityDetails(learningUtilityDetails.Id))).ToList();
            return View(wishlistViewModels);
        }

        public ActionResult Add(int id, Wishlist wishlist)
        {
            LearningUtilityDetails item = GetLearningUtilityDetails(id);
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
            LearningUtilityDetails item = GetLearningUtilityDetails(id);
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
            return RedirectToAction("Index");
        }

        private LearningUtilityDetails GetLearningUtilityDetails(int id)
        {
            return learningUtilityDetailsRepository.FindBy(id);
        }
    }
}