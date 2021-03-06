﻿using System;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public string Picture { get; set; }
        [Display(Name = "Leermiddel")]
        public string Name { get; set; }
        [Display(Name = "Week van")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date { get; set; }
        [Display(Name = "Aantal gereserveerd")]
        public int AmountWanted { get; set; }
        [Display(Name = "Huidige status")]
        public Reservation Reservation { get; set; }
        [Display(Name = "Reservatie status")]
        public string State { get; set; }

        public ReservationViewModel()
        {
            
        }

        public ReservationViewModel(Reservation reservation)
        {
            Id = reservation.LearningUtility.Id;
            Picture = reservation.LearningUtility.Picture;
            Name = reservation.LearningUtility.Name;
            Date = reservation.DateWanted;
            AmountWanted = reservation.Amount;
            Reservation = reservation;
            State = reservation.GetState();
        }
    }
}