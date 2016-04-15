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
using System.Globalization;

namespace DidactischeLeermiddelen.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private ILearningUtilityRepository learningUtilityRepository;
        private IReservationRepository reservationRepository;

        /// <summary>
        /// Parameter constructor
        /// </summary>
        /// <param name="reservationRepository"></param>
        /// <param name="learningUtilityRepository"></param>
        public ReservationController(IReservationRepository reservationRepository,ILearningUtilityRepository learningUtilityRepository)
        {
            this.reservationRepository = reservationRepository;
            this.learningUtilityRepository = learningUtilityRepository;
        }

        /// <summary>
        /// Returns all the reservations for a specific user in the future, sorted by Date (asc). 
        /// If the list is empty, returns the empty reservation screen.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult Index(User user)
        {

            IEnumerable<Reservation> reservations = reservationRepository.FindAllForUser(user.EmailAddress);
            
            if (!reservations.Any())
                return View("EmptyReservations");

            reservations = sortReservations(reservations);

            IEnumerable<ReservationViewModel> reservationViewModels = MapToViewModels(reservations);

            return View(reservationViewModels);
        }

        /// <summary>
        /// Maps the reservations to the ReservationViewModel
        /// Beware : Date is not correct yet!!
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private IEnumerable<ReservationViewModel> MapToViewModels(IEnumerable<Reservation> reservations)
        {
            return reservations.Select(r => new ReservationViewModel(reservationRepository.FindBy(r.Id))).ToList();
        }

        /// <summary>
        /// Sorts the reservations ascending by week
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private IEnumerable<Reservation> sortReservations(IEnumerable<Reservation> reservations)
        {
            return reservations.OrderBy(res => res.DateWanted);
        }

        /// <summary>
        /// Adds a reservation for a learning utility by a specific user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="wishlistViewModels"></param>
        /// <returns></returns>
        public ActionResult Add(User user, IEnumerable<WishlistViewModel> wishlistViewModels)
        {
            bool save = true;
            foreach (var wishlistViewModel in wishlistViewModels)
            {                
                LearningUtility learningUtility = learningUtilityRepository.FindBy(wishlistViewModel.Id);

                DateTime dateWanted = (DateTime)wishlistViewModel.Date;
                int amount = wishlistViewModel.AmountWanted;


                try
                {
                    user.AddReservation(dateWanted, amount, learningUtility);
                    if(user is Lector && amount > learningUtility.AmountAvailableForWeek(dateWanted))
                    {
                        DeleteStudentReservations(learningUtility);
                    }

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

                reservationRepository.SaveChanges();
                learningUtilityRepository.SaveChanges();
                TempData["info"] = "Reservatie geslaagd";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Wishlist");
        }

        /// <summary>
        /// Deletes a reservation
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        public ActionResult Delete(int reservationId)
        {
            try
            {
                Reservation reservation = reservationRepository.FindBy(reservationId);
                User user = reservation.User;


                if (reservation == null)
                    return HttpNotFound();

                user.RemoveReservation(reservation);
                reservationRepository.Delete(reservation);
                reservationRepository.SaveChanges();
                learningUtilityRepository.SaveChanges();

                TempData["info"] = String.Format("Reservatie werd succesvol verwijderd.");

            }
            catch (Exception)
            {
                TempData["error"] = "Verwijderen van de reservatie is mislukt, gelieve opnieuw te proberen. " +
                           "Indien de problemen zich blijven voordoen, contacteer de  administrator.";
            }
            return RedirectToAction("Index");

        }



        /// <summary>
        /// For the given learningUtlity, delete reservations made by students where the amount has become 0 after 
        /// a lector has blocked their material.
        /// </summary>
        /// <param name="learningUtility"></param>
        private void DeleteStudentReservations(LearningUtility learningUtility)
        {
            var ReservationsToDelete = learningUtility.Reservations.Where(res => res.Amount <= 0);

            foreach( Reservation r in ReservationsToDelete.ToList())
            {
                r.LearningUtility.Reservations.Remove(r);
                r.User.Reservations.Remove(r);
                reservationRepository.Delete(r);

                
            }
        }
    }
}