using DidactischeLeermiddelen.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class LearningUtilityDetails
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
        /// Sets the price of the LearningUtility
        /// Optional, if set allows Decimal.Zero has to be postive.
        /// Null values is converted to Decimal.Zero
        /// <exception cref="ValidationException"></exception>
        /// ^           # Start of string.
        /// [0-9]+      # Must have one or more numbers.
        /// (           # Begin optional group.
        /// \.          # The decimal point, . must be escaped, # or it is treated as "any character".
        /// [0-9]{1,2}  # One or two numbers.
        /// )?          # End group, signify it's optional with ?
        /// $           # End of string.
        /// </summary>
        public decimal Price
        {

                get { return price; }
                set
                {
                    if(value < 0 )
                        throw new ArgumentException("error");
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
        public virtual ICollection<LearningUtilityReservation> LearningUtilityReservations { get; set; }
        public Byte[] TimeStamp { get; set; }
        #endregion

        #region Constructors

        public LearningUtilityDetails()
        {
            FieldsOfStudy = new List<FieldOfStudy>();
            TargetGroups = new List<TargetGroup>();
            LearningUtilityReservations = new List<LearningUtilityReservation>();
            Loanable = true;
            Price = 0;
        }

        public LearningUtilityDetails(string name, string description, Location location):this()
        {
            Name = name;
            Description = description;
            Location = location;
        }

        #endregion
        #region Methods

        public int AmountAvailableForWeek(int week, int currentWeek)
        {
            IEnumerable<LearningUtilityReservation> reservations = LearningUtilityReservations.Where(r => r.Week == week || r.Week < currentWeek);
            return AmountInCatalog - reservations.Sum(r => r.Amount) - AmountUnavailable;
        }

        public int AmountReservedForWeek(int week)
        {
            return LearningUtilityReservations.Where(r => r.Week == week && r.User.GetType() == typeof (Student)).Sum(r => r.Amount);
        }

        public int AmountBlockedForWeek(int week)
        {
            return LearningUtilityReservations.Where(r => r.Week == week && r.User.GetType() == typeof (Lector)).Sum(r => r.Amount);
        }

        public int AmountUnavailableForWeek(int week, int currentWeek)
        {
            IEnumerable<LearningUtilityReservation> reservations = LearningUtilityReservations.Where(r => r.Week == week || r.Week < currentWeek);
            return reservations.Sum(r => r.Amount) + AmountUnavailable;
        }
        #endregion
    }
}