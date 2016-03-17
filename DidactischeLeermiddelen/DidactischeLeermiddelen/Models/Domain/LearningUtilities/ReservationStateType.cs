using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public enum ReservationStateType
    {
        [Display(Name = "Te laat")]
        Late,
        [Display(Name = "Gereserveerd")]
        Reserved,
        [Display(Name = "Geblockeerd")]
        Blocked


    }
}