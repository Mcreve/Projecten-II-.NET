using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Properties;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class Company
    {
        #region Fields
        private string name;
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

        #endregion
    }
}