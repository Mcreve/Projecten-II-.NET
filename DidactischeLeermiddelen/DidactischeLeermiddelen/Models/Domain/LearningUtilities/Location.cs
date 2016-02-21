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

        public Location()
        {
            
        }
        public Location(string name):this()
        {
            Name = name;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

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