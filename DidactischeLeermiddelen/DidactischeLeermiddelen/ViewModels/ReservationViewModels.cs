using System;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Leermiddel")]
        public string Name { get; set; }
        public int AmountBlocked { get; set; }
        [Display(Name = "Reservatie week")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date { get; set; }
        [Display(Name = "Aantal gewenst")]
        public int AmountWanted { get; set; }

        public ReservationViewModel()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="learningUtility"></param>
        public ReservationViewModel(LearningUtility learningUtility) : this()
        {
            
            Name = learningUtility.Name;

        }
    }
}