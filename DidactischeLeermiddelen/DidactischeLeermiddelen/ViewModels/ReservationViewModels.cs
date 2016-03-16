﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public string Picture { get; set; }
        [Display(Name = "Leermiddel")]
        public string Name { get; set; }
        [Display(Name = "Reservatie week")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date { get; set; }
        [Display(Name = "Aantal gewenst")]
        public int AmountWanted { get; set; }
        public Reservation Reservation { get; set; }
        public ReservationViewModel()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="learningUtility"></param>
        public ReservationViewModel(Reservation reservation) : this()
        {
            Reservation = reservation;
            Id = reservation.LearningUtility.Id;
            Name = reservation.LearningUtility.Name;
            Picture = reservation.LearningUtility.Picture;
            Date = FirstDateOfWeek(DateTime.Now.Year, reservation.Week);
            AmountWanted = reservation.Amount;
        }
        private static DateTime FirstDateOfWeek(int year, int weekOfYear)
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
        }
    }
}