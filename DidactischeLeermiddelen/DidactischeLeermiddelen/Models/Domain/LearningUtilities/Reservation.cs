using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Properties;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public abstract class Reservation
    {

        public int Id { get; set; }
        public DateTime DateWanted { get; set; }
        public virtual User User { get; set; }
        public int Amount { get; set; }
        public virtual LearningUtility LearningUtility { get; set; }
        public DateTime ReservationDate { get; set; }
        public string DaysBlocked { get; set; }

        public string GetState()
        {
            int currentWeek = LearningUtility.GetCurrentWeek(DateTime.Now);
            int weekReserved = LearningUtility.GetCurrentWeek(DateWanted);
            if (currentWeek > weekReserved)
            {
                return "Te laat";
            }
            var userType = User as Student;
            if (userType != null)
            {
                return "Gereserveerd";
            }
            return "Geblokkeerd";
        }
    }
}