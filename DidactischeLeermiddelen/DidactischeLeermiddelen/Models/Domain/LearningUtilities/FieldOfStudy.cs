using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Properties;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class FieldOfStudy
    {
        #region Fields

        private string name;

        #endregion

        #region Properties

        public int Id { get; set; }
        /// <summary>
        /// Sets the name of the FieldOfStudy 
        /// Required: Min 1 Character, Max 100 Characters
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [DisplayName(@"Leergebied")]
        [Required(ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "FieldOfStudyNameRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "FieldOfStudyNameRegex")]
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
        public FieldOfStudy()
        {
        }
        /// <summary>
        /// Constructor with 1 parameter, calls default constructor
        /// </summary>
        /// <param name="name"></param>
        public FieldOfStudy(string name) : this()
        {
            Name = name;
        }

        #endregion
    }
}