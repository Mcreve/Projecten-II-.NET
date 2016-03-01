using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

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
            return View(wishlist.LearningUtilities);
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