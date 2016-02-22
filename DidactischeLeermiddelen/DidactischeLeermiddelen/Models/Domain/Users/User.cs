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
        #region Methods

        /// <summary>
        /// Returns the LearningUtilityDetails in a IQueryable which a specific user can see
        /// <seealso cref="Student"/>
        /// <seealso cref="Lector"/>
        /// </summary>
        /// <param name="learningUtilityDetailsRepository"></param>
        /// <returns>Student or Lector object</returns>
        public abstract IQueryable<LearningUtilityDetails> GetLearningUtilities(
            ILearningUtilityDetailsRepository learningUtilityDetailsRepository);

        #endregion

        #region Fields

        private string firstName;
        private string lastName;
        private string emailAddress;

        #endregion

        #region Properties

        public int Id { get; set; }
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

        #endregion
    }
}