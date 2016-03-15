using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Infrastructure;
using DidactischeLeermiddelen.Models;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using DidactischeLeermiddelen.ViewModels;

namespace DidactischeLeermiddelen.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private ILearningUtilityRepository learningUtilityRepository;
        public ReservationController(ILearningUtilityRepository learningUtilityRepository)
        {
            this.learningUtilityRepository = learningUtilityRepository;
        }

        public ActionResult Index(User user)
        {
            IEnumerable<LearningUtility> reservations = FindAllLearningUtilitiesReservedByUser(user);

            if (!reservations.Any())
                return View("EmptyReservations");

            IEnumerable <ReservationViewModel> reservationViewModels =
                reservations.Select(utility => new ReservationViewModel(utility)).ToList();
             
            return View(reservationViewModels);
        }



        public ActionResult Add(User user, IEnumerable<WishlistViewModel> wishlistViewModels)
        {
            bool save = true;
            foreach (var wishlistViewModel in wishlistViewModels)
            {                
                LearningUtility learningUtility = GetLearningUtility(wishlistViewModel.Id);
                LearningUtilityReservation reservation = new LearningUtilityReservation
                {
                    User = user,
                    Week = learningUtility.GetCurrentWeek((DateTime)wishlistViewModel.Date),
                    Amount = wishlistViewModel.AmountWanted
                };
                try { 
                    learningUtility.AddReservation(reservation);
                    
                } catch (ArgumentOutOfRangeException)
                {
                    save = false;
                    TempData["error"] = "Er ging iets fout met je reservatie. Controleer je aantal gewenste items met het aantal beschikbare items en probeer opnieuw.";
                } catch (ArgumentNullException)
                {
                    save = false;
                    TempData["error"] = "Er ging iets fout met je reservatie. Je moet minstens 1 item reserveren voor elk gewenst item. Controleer je gewenste aantallen en probeer opnieuw";
                }                               
            }
            if (save) { 
                learningUtilityRepository.SaveChanges();
                TempData["info"] = "Reservatie geslaagd";

                return View(wishlistViewModels);
            }
            return RedirectToAction("Index", "Wishlist");
        }

        private LearningUtility GetLearningUtility(int id)
        {
            return learningUtilityRepository.FindBy(id);
        }

        private IEnumerable<LearningUtility> FindAllLearningUtilitiesReservedByUser(User user)
        {
            IEnumerable<LearningUtility> result =
                learningUtilityRepository.FindAll().Where(
                    (
                        learningUtility =>
                            learningUtility.LearningUtilityReservations.Any(
                                reservation => reservation.User.EmailAddress == user.EmailAddress)));
            return result;

        }
        public ActionResult Delete(int learningUtilityId, int reservationId)
        {
            try
            {
                LearningUtility learningUtility = learningUtilityRepository.FindBy(learningUtilityId);
                LearningUtilityReservation reservation =
                    learningUtility.LearningUtilityReservations.FirstOrDefault(res => res.Id == reservationId);

                if (reservation == null)
                    return HttpNotFound();

                learningUtility.RemoveReservation(reservationId);
                learningUtilityRepository.SaveChanges();
                TempData["info"] = String.Format("Reservatie van item {0} werd verwijderd", learningUtility.Name);
            }
            catch (Exception)
            {
                TempData["error"] = "Verwijderen reservatie mislukt. Probeer opnieuw. " +
                           "Als de problemen zich blijven voordoen, contacteer de  administrator.";
            }


            return RedirectToAction("Index");
        }

       }
}