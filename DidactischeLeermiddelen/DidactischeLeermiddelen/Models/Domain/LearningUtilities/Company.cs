using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.Users;
using DidactischeLeermiddelen.Properties;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class Company
    {
        #region Fields
        private string name;
        private string website;
        private string contactPersonName;
        private string emailAddress;
        #endregion

        #region Propterties

        public int Id { get; set; }
        /// <summary>
        /// Sets the name of the Company 
        /// Required: Min 1 Character, Max 100 Characters
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [DisplayName(@"Bedrijf")]
        [Required(ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "CompanyNameRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "CompanyNameRegex")]
        public string Name
        {
            get { return name; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) {MemberName = "Name"});
                name = value;
            }
        }
        [DisplayName(@"Website")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = @"CompanyWebsiteRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "CompanyWebsiteRegex")]
        public string Website
        {
            get { return website; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "Website" });
                website = value;
            }
        }
        [DisplayName(@"Contact persoon")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "CompanyContactPersonNameRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "CompanyContactPersonNameRegex")]
        public string ContactPersonName
        {
            get { return contactPersonName; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "ContactPersonName" });
                contactPersonName = value;
            }
        }
        [DisplayName(@"E-mail")]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "CompanyEmailAddressRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "CompanyEmailAddressRegex")]
        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "EmailAddress" });
                emailAddress = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Company()
        {
        }
        /// <summary>
        /// Constructor with 1 parameter, calls default constructor
        /// </summary>
        /// <param name="name"></param>
        public Company(string name) : this()
        {
            Name = name;
        }

        public Company(string name,string website, string contactPersonName, string emailAddress) : this()
        {
            Name = name;
            Website = website;
            ContactPersonName = contactPersonName;
            EmailAddress = emailAddress;
        }

        #endregion
    }
}