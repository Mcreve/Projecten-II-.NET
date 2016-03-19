using DidactischeLeermiddelen.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class LearningUtility
    {

        #region Fields

        private string name;
        private string description;
        private decimal price;
        private string articleNumber;
        private string picture;
        private Location location;
      
        #endregion


        #region Properties
        public int Id { get; set; }
        /// <summary>
        /// Sets the name of the LearningUtility
        /// Required, Min 1 Character, Max 100 Characters, allows alphanumeric
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [Display(Name = "Naam")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "LearningUtilityNameRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "LearningUtilityNameRegex")]
        public string Name
        {
            get { return name; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "Name" });
                name = value;
            }

        }

        
        /// <summary>
        /// Sets the description of the LearningUtility
        /// Required, Min 1 Character, Max 1000 Characters, allows alphanumeric
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [Display(Name = "Omschrijving")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "LearningUtilityDescriptionRegex")]
        [RegularExpression(@"(?i).{1,1000}",
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "LearningUtilityDescriptionRegex")]
        public string Description {
            get { return description; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "Description" });
                description = value;
            }
        }

        /// <summary>
        /// Sets the price of the LearningUtility, cannot be negative.
        /// <exception cref="ArgumentException"></exception>
        public decimal Price
        {

                get { return price; }
                set
                {
                    if(value < 0 )
                        throw new ArgumentException(Resources.LearningUtilityPriceRegex);
                price = value;
                }
            
        }
        /// <summary>
        /// Sets the loanable status of the LearningUtility
        /// Initialized to True, by default in the constructor
        /// </summary>
        [Display(Name = "Uitleenbaar")]
        public bool Loanable { get; set; }

        /// <summary>
        /// Sets the articleNumber of the LearningUtility
        /// Optional, Min 1 Character, Max 100 Characters, allows alphanumeric and null
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [Display(Name = "Artikel Nr.")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "LearningUtilityArticleNumberRegex")]
        public string ArticleNumber {

            get { return articleNumber; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "ArticleNumber" });
                articleNumber = value;
            }
        }
        [Display(Name = "Leergebied")]
        public virtual ICollection<FieldOfStudy> FieldsOfStudy { get; set; }    
        [Display(Name = "Doelgroep")]
        public virtual ICollection<TargetGroup> TargetGroups { get; set; }
        [Display(Name = "Bedrijf")]
        public virtual Company Company { get; set; }
        [Display(Name = "Locatie")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "LearningUtilityLocationRegex")]
        public virtual Location Location {
            get { return location; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "Location" });
                location = value;
            }
        }

        /// <summary>
        /// Sets the picture of the LearningUtility
        /// Optional, Min 1 Character, Max 100 Characters, allows alphanumeric and null
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [Display(Name = "Afbeelding")]
        [RegularExpression(@"(?i).{0,250}",
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "LearningUtilityPictureRegex")]

        public string Picture
        {
            get { return picture; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "Picture" });
                picture = value;
            }
        }

        public int AmountInCatalog { get; set; }
        public int AmountUnavailable { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
        public DateTime? DateWanted { get; set; }
        public Byte[] TimeStamp { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LearningUtility()
        {
            FieldsOfStudy = new List<FieldOfStudy>();
            TargetGroups = new List<TargetGroup>();
            Reservations = new List<Reservation>();
            Loanable = true;
            Price = 0;
        }
        /// <summary>
        /// Constructor with parameters, calls default constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="location"></param>
        public LearningUtility(string name, string description, Location location):this()
        {
            Name = name;
            Description = description;
            Location = location;
        }

        #endregion
        #region Methods
        /// <summary>
        /// Gets the amount of items availble for the specific week. 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int AmountAvailableForWeek(DateTime date)
        {
            int week = GetCurrentWeek(date);
            int currentWeek = GetCurrentWeek(DateTime.Now);
            IEnumerable<Reservation> reservations = Reservations.Where(r =>GetCurrentWeek(r.DateWanted) == week ||GetCurrentWeek(r.DateWanted) < currentWeek);
            return AmountInCatalog - reservations.Sum(r => r.Amount) - AmountUnavailable;
        }
        /// <summary>
        /// Gets the amount of the items that are reserved(by student) for a specific week.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int AmountReservedForWeek(DateTime date)
        {
            int week = GetCurrentWeek(date);
            return Reservations.Where(r =>GetCurrentWeek(r.DateWanted) == week && r.User.GetType() == typeof (Student)).Sum(r => r.Amount);
        }
        /// <summary>
        /// Gets the amount of the items that are blocked(by lector) for a specific week.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int AmountBlockedForWeek(DateTime date)
        {
            int week = GetCurrentWeek(date);
            
            IEnumerable<Reservation> reservations =  Reservations.Where(r =>GetCurrentWeek(r.DateWanted) == week && r.User.GetType() == typeof (Lector));
            return reservations.Sum(r => r.Amount);
        }
        /// <summary>
        /// Gets the amount of the items that are unavailble for a specific week.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int AmountUnavailableForWeek(DateTime date)
        {
            int week = GetCurrentWeek(date);
            int currentWeek = GetCurrentWeek(DateTime.Now);
            IEnumerable<Reservation> reservations = Reservations.Where(r => GetCurrentWeek(r.DateWanted) == week ||GetCurrentWeek(r.DateWanted) < currentWeek);
            return reservations.Sum(r => r.Amount) + AmountUnavailable;
        }

        /// <summary>
        /// Create a reservation for the specific learningutility.
        /// </summary>
        /// <param name="reservation"></param>
        public void AddReservation(Reservation reservation)
        {
            
            if (reservation.Amount > AmountAvailableForWeek(reservation.DateWanted))
                throw new ArgumentOutOfRangeException();
            if (reservation.Amount > 0)
            {
                this.Reservations.Add(reservation);
            } else
            {
                throw new ArgumentNullException("Meer dan 1 item nodig om te reserveren");
            }      
        }
        /// <summary>
        /// Removes the reservation from the reservations for the Learning Utility
        /// </summary>
        /// <param name="reservation"></param>
        public void RemoveReservation(Reservation reservation)
        {
            Reservations.Remove(reservation);
        }
        /// <summary>
        /// Returns the integer of the current week.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int GetCurrentWeek(DateTime date)
        {
            Calendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
        }
        #endregion
    }
}