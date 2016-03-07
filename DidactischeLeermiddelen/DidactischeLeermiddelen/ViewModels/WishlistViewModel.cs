using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.ViewModels
{
    public class WishlistViewModel
    {
        private int week;
        private int currentWeek;
        private Calendar calendar = new GregorianCalendar();

        public int Week { get { return week; } set { week = value; } }
        public int Id { get; set; }
        [Display(Name = "Leermiddel")]
        public string Name { get; set; }
        [Display(Name = "Beschikbaar")]
        public int AmountInStock { get; set; }
        [Display(Name = "Totaal")]
        public int AmountInCatalog { get; set; }
        [Display(Name = "Tijdelijk onbeschikbaar")]
        public int AmountUnavailable { get; set; }
        [Display(Name = "Gereserveerd")]
        public int AmountBlocked { get; set; }
        [Display(Name = "Selecteer de datum wanneer u de items wenst te gebruiken")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        [Display(Name = "Aantal gewenst")]
        public int AmountWanted { get; set; }

        public WishlistViewModel()
        {
            currentWeek = GetWeekOfYear(DateTime.Now);
        }
        public WishlistViewModel(LearningUtilityDetails learningUtilityDetails) : this()
        {
            Id = learningUtilityDetails.Id;
            Name = learningUtilityDetails.Name;
            Date = learningUtilityDetails.DateWanted ?? DateTime.Now;
            if(Date != null)
                week = GetWeekOfYear((DateTime) Date);
            AmountInCatalog = learningUtilityDetails.AmountInCatalog;
            AmountUnavailable = learningUtilityDetails.AmountUnavailableForWeek(week,currentWeek);
            AmountBlocked = learningUtilityDetails.AmountBlockedForWeek(week);
            AmountInStock = learningUtilityDetails.AmountAvailableForWeek(week, currentWeek);
        }

        private int GetWeekOfYear(DateTime date)
        {
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Saturday);
        }
    }
}