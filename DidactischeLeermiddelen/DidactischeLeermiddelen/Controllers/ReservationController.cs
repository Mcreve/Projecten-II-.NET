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
            bool save = true;
            foreach (var wishlistViewModel in wishlistViewModels)
            {                
                LearningUtilityDetails learningUtilityDetails = GetLearningUtilityDetails(wishlistViewModel.Id);
                LearningUtilityReservation reservation = new LearningUtilityReservation
                {
                    User = user,
                    Week = learningUtilityDetails.GetCurrentWeek((DateTime)wishlistViewModel.Date),
                    Amount = wishlistViewModel.AmountWanted
                };
                try { 
                    learningUtilityDetails.AddReservation(reservation);
                    
                } catch (ArgumentOutOfRangeException e)
                {
                    save = false;
                    TempData["error"] = "Er ging iets fout met je reservatie. Controleer je aantal gewenste items en probeer opnieuw.";
                } catch (ArgumentNullException e)
                {
                    save = false;
                    TempData["error"] = "Er ging iets fout met je reservatie. Je moet minstens 1 item reserveren voor " + learningUtilityDetails.Name;
                }                               
            }
            if (save) { 
                learningUtilityDetailsRepository.SaveChanges();
                return View(wishlistViewModels);
            }
            return RedirectToAction("Index", "Wishlist");
        }

        private LearningUtilityDetails GetLearningUtilityDetails(int id)
        {
            return learningUtilityDetailsRepository.FindBy(id);
        }
    }
}