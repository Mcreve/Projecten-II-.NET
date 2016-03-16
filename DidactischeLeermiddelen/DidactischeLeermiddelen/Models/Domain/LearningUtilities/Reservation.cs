using DidactischeLeermiddelen.Models.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public virtual User User { get; set; }
        public virtual LearningUtility LearningUtility { get; set; }
        public int Amount { get; set; }
    }
}