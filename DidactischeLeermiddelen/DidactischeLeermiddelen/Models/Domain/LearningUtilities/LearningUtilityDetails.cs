using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;
using DidactischeLeermiddelen.Models.Domain.Users;
using DidactischeLeermiddelen.Properties;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class LearningUtilityDetails
    {

        #region Fields

        private string name;
        private string description;
        private decimal? price;
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
        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "{0:c}")]
       // [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$",
       //  ErrorMessageResourceType = typeof(Resources),
       //ErrorMessageResourceName = "LearningUtilityPriceRegex")]
        public decimal? Price
        {

                get { return price; }
                set
                {
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
        public virtual FieldOfStudy FieldOfStudy { get; set; }    
        [Display(Name = "Doelgroep")]
        public virtual TargetGroup TargetGroup { get; set; }
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
        [Display(Name = "Leermiddel")]
        public virtual ICollection<LearningUtility> LearningUtilities { get; set; }
        #endregion

        #region Constructors

        public LearningUtilityDetails()
        {
            LearningUtilities = new List<LearningUtility>();
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

        /// <summary>
        /// Adds a new LearningUtility to the collection LearningUtilities
        /// </summary>
        /// <param name="stateType"></param>
        /// <param name="reservedBy"></param>
        /// <param name="lendTo"></param>
        public void AddLearningUtilty(StateType stateType, User reservedBy, User lendTo)
        {
            LearningUtility newLearningUtility = new LearningUtility();
            newLearningUtility.StateType = stateType;
            newLearningUtility.ReservedBy = reservedBy;
            newLearningUtility.LendTo = lendTo;
            LearningUtilities.Add(newLearningUtility);
            
            
        }
        #endregion
    }
}