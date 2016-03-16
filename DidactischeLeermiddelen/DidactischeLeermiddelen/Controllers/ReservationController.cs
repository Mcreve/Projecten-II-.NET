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

            IQueryable<Reservation> reservations = reservationRepository.FindAllForUser(user.EmailAddress);
            
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
        private IEnumerable<ReservationViewModel> MapToViewModels(IQueryable<Reservation> reservations)
        {
            return (from reservation in reservations
             select new ReservationViewModel
             {
                 Reservation = reservation,
                 Id = reservation.LearningUtility.Id,
                 Name = reservation.LearningUtility.Name,
                 Picture = reservation.LearningUtility.Picture,
                 Date = DateTime.Now,
                 AmountWanted = reservation.Amount
             }).ToList();
        }

        /// <summary>
        /// Sorts the reservations ascending by week
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private IQueryable<Reservation> sortReservations(IQueryable<Reservation> reservations)
        {
            return reservations.OrderBy(res => res.Week);
        }

        /// <summary>
        /// Adds a reservation for a learning utility by a specific user, clears the old wishlist.
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

                if (reservation == null)
                    return HttpNotFound();

                reservationRepository.Delete(reservation);
                reservationRepository.SaveChanges();

                TempData["info"] = String.Format("Reservatie werd succesvol verwijderd.");

            }
            catch (Exception)
            {
                TempData["error"] = "Verwijderen van de reservatie is mislukt, gelieve opnieuw te proberen. " +
                           "Indien de problemen zich blijven voordoen, contacteer de  administrator.";
            }
            return RedirectToAction("Index");

        }
        /*private static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }*/
    }
}