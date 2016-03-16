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
        private IUserRepository userRepository;


        public ReservationController(ILearningUtilityRepository learningUtilityRepository,IUserRepository userRepository)
        {
            this.learningUtilityRepository = learningUtilityRepository;
            this.userRepository = userRepository;
        }

        public ActionResult Index(User user)
        {
            ICollection<Reservation> reservations = user.Reservations;
            
            if (!reservations.Any())
                return View("EmptyReservations");



            IEnumerable <ReservationViewModel> reservationViewModels =
               reservations.Select(res => new ReservationViewModel(res)).ToList();
             
            return View(reservationViewModels);
        }

        public ActionResult Add(User user, IEnumerable<WishlistViewModel> wishlistViewModels)
        {
            bool save = true;
            foreach (var wishlistViewModel in wishlistViewModels)
            {                
                LearningUtility learningUtility = learningUtilityRepository.FindBy(wishlistViewModel.Id);

                int week = learningUtility.GetCurrentWeek((DateTime)wishlistViewModel.Date);
                int amount = wishlistViewModel.AmountWanted;

                try
                {
                    user.AddReservation(week, amount, learningUtility);
                    user.Wishlist.RemoveItem(learningUtility);
                }
                catch (ArgumentOutOfRangeException)
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
                userRepository.SaveChanges();
                TempData["info"] = "Reservatie geslaagd";

                return View(wishlistViewModels);
            }
            return RedirectToAction("Index", "Wishlist");
        }

        public ActionResult Delete(User user, int reservationId)
        {

                Reservation reservation =  user.FindReservation(reservationId);
                LearningUtility learningUtility = reservation.LearningUtility;

                if (reservation == null)
                    return HttpNotFound();

                learningUtility.RemoveReservation(reservation);
                user.RemoveReservation(reservation);
                userRepository.SaveChanges();
                learningUtilityRepository.SaveChanges();

                TempData["info"] = String.Format("Reservatie werd verwijderd.");

            return RedirectToAction("Index");
        }

       }
}