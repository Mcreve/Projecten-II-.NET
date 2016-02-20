using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Properties;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public abstract class User
    {
        #region Methods

        public abstract IList<LearningUtilityDetails> GetLearningUtilities(
            ILearningUtilityDetailsRepository learningUtilityDetailsRepository);

        #endregion

        #region Fields

        private string firstName;
        private string lastName;
        private string emailAddress;

        #endregion

        #region Properties

        public int Id { get; set; }

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