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
        [Display(Name = "Geblokkeerd")]
        public int AmountBlocked { get; set; }
        [Display(Name = "Reservatie week")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date { get; set; }
        [Display(Name = "Aantal gewenst")]
        public int AmountWanted { get; set; }

        public WishlistViewModel()
        {
        }
        public WishlistViewModel(LearningUtility learningUtility) : this()
        {
            if(learningUtility.DateWanted != null)
            {
                Date = learningUtility.DateWanted;
            } else
            {
                var date = DateTime.Now;
                if(date.DayOfWeek == DayOfWeek.Friday && date.Hour > 17)
                {
                    Date = DateTime.Now.AddDays(3);
                }
                else if(date.DayOfWeek == DayOfWeek.Saturday)
                {
                    Date = DateTime.Now.AddDays(2);
                }
                else if(date.DayOfWeek == DayOfWeek.Sunday)
                {
                    Date = DateTime.Now.AddDays(1);
                }
                else
                {
                    Date = DateTime.Now;
                }
            }
            Id = learningUtility.Id;
            Name = learningUtility.Name;            
            AmountInCatalog = learningUtility.AmountInCatalog;
            AmountUnavailable = learningUtility.AmountUnavailableForWeek((DateTime)Date);
            AmountBlocked = learningUtility.AmountBlockedForWeek((DateTime)Date);
            AmountInStock = learningUtility.AmountAvailableForWeek((DateTime)Date);
        }

    }
}