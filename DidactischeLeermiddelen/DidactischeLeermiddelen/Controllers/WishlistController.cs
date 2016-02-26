using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Controllers
{
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
            return View(wishlist.WishlistLines);
        }

        public ActionResult Add(int id, Wishlist wishlist)
        {
            LearningUtilityDetails item = GetLearningUtilityDetails(id);
            if (item != null)
            {
                try
                {
                    wishlist.AddLine(item);
                    TempData["Info"] = item.Name + " werd toegevoegd aan uw verlanglijst.";
                }
                catch (InvalidOperationException e)
                {
                    TempData["Error"] = e.Message;
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
                    wishlist.RemoveLine(item);
                    TempData["Info"] = item.Name + " werd van uw verlanglijstje verwijderd";
                }
                catch (InvalidOperationException e)
                {
                    TempData["Error"] = e.Message;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Plus(int id, Wishlist wishlist)
        {
            LearningUtilityDetails item = GetLearningUtilityDetails(id);
            if (item != null)
            {
                try
                {
                    wishlist.IncreaseQuantity(item.Id);
                    TempData["Info"] = "Het aantal van " + item.Name + " werd succesvol verhoogd";
                }
                catch (InvalidOperationException e)
                {
                    TempData["Error"] = e.Message;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Min(int id, Wishlist wishlist)
        {
            LearningUtilityDetails item = GetLearningUtilityDetails(id);
            if (item != null)
            {
                try
                {
                    wishlist.DecreaseQuantity(item.Id);
                    TempData["Info"] = "Het aantal van " + item.Name + " werd succesvol verlaagd";
                }
                catch (InvalidOperationException e)
                {
                    TempData["Error"] = e.Message;
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