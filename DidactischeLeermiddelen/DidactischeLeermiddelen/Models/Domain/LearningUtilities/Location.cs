using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Properties;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class Location
    {
        #region Fields
        private string name;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Location()
        {
            
        }
        /// <summary>
        /// Constructor with 1 parameter, calls default constructor
        /// </summary>
        /// <param name="name"></param>
        public Location(string name):this()
        {
            Name = name;
        }

        #endregion

        #region Properties

        public int Id { get; set; }
        /// <summary>
        /// Sets the name of the Location 
        /// Required: Min 1 Character, Max 100 Characters
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [DisplayName(@"Locatie")]
        [Required(ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "LocationNameRegex")]
        [RegularExpression(@"(?i).{1,100}",
            ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "LocationNameRegex")]
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
    }
}