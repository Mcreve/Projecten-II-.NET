using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Infrastructure;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using DidactischeLeermiddelen.ViewModels;

namespace DidactischeLeermiddelen.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private ILearningUtilityDetailsRepository learningUtilityDetailsRepository;
        public ReservationController(ILearningUtilityDetailsRepository learningUtilityDetailsRepository)
        {
            this.learningUtilityDetailsRepository = learningUtilityDetailsRepository;
        }
        public ActionResult Add(User user, IEnumerable<WishlistViewModel> wishlistViewModels)
        {
            wishlistViewModels.Select(w => new LearningUtilityReservation
            {
                Amount = w.AmountWanted,
                Id = w.Id,
                User = user,
                Week = w.Week
            });

            return RedirectToAction("Index", "Wishlist");
        }
    }
}