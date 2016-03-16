using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Properties;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public abstract class User
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// Creates a subclass Student or Lector
        /// </summary>
        protected User()
        {
            
        }
        /// <summary>
        /// Constructor with 3 parameter, calls default constructor
        /// Creates a subclass Student or Lector
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        protected User(string firstName, string lastName, string emailAddress):this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
        }
        #endregion

        #region Fields

        private string firstName;
        private string lastName;
        private string emailAddress;

        #endregion

        #region Properties
        /// <summary>
        /// Sets the first name of the User 
        /// Required: Min 1 Character, Max 100 Characters
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [DisplayName(@"Voornaam")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "UserFirstNameRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "UserFirstNameRegex")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) {MemberName = "FirstName"});
                firstName = value;
            }
        }
        /// <summary>
        /// Sets the last name of the User 
        /// Required: Min 1 Character, Max 100 Characters
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [DisplayName(@"Achternaam")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "UserLastNameRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "UserLastNameRegex")]
        public string LastName
        {
            get { return lastName; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) {MemberName = "LastName"});
                lastName = value;
            }
        }
        /// <summary>
        /// Sets the Email Address of the User 
        /// Required: Min 1 Character, Max 100 Characters before hogent.be
        /// Has to end with "hogent.be"
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [DisplayName(@"E-mail")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "UserEmailRegex")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@".{1,100}(?i)(hogent\.be)$",
            ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "UserEmailRegex")]
        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) {MemberName = "EmailAddress"});
                emailAddress = value;
            }
        }
        public virtual Wishlist Wishlist { get; set; }
        public virtual ICollection<LearningUtilityReservation>  Reservations { get; set; }
        #endregion

        #region methods
        public void AddReservation(int week, int amount, LearningUtility learningUtility)
        {
            LearningUtilityReservation reservation = new LearningUtilityReservation()
            {
                User = this,
                Week = week,
                Amount = amount,
                LearningUtility = learningUtility
            };
            Reservations.Add(reservation);
        }
        public LearningUtilityReservation FindReservation(int reservationId)
        {
            return Reservations.FirstOrDefault(res => res.Id == reservationId);
        }

        public void RemoveReservation(LearningUtilityReservation reservation)
        {
            Reservations.Remove(reservation);
        }
        #endregion
    }
}