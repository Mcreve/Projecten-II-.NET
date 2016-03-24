using DidactischeLeermiddelen.Models.Domain.Users;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class Reservation
    {
        public int Id { get; set; }
        [Display(Name = "Op Datum")] 
        public DateTime DateWanted { get; set; }
        public virtual User User { get; set; }
        [Display(Name = "Aantal stuks")]
        public int Amount { get; set; }
        public virtual LearningUtility LearningUtility { get;  set; }
        public DateTime? ReservationDate { get; set; }
        public string DaysBlocked { get; set; }

        #region Methods
        /// <summary>
        /// Gets the state of the reservation in essence of the current week and the reservation date of the reservation.
        /// </summary>
        /// <returns>The state of the reservation </returns>
        public string GetState()
        {
            int currentWeek = LearningUtility.GetCurrentWeek(DateTime.Now);
            int weekReserved = LearningUtility.GetCurrentWeek(DateWanted);
            if (currentWeek > weekReserved)
            {
                return ReservationStateType.Late.ToString();
            }
            var userType = User as Student;
            if (userType != null)
            {
                return ReservationStateType.Reserved.ToString();
            }
            return ReservationStateType.Blocked.ToString();
        }
        #endregion
    }
}