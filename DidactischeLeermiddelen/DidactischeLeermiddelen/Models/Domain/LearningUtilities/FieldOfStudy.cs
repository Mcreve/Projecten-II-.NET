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

        public FieldOfStudy()
        {
        }

        public FieldOfStudy(string name) : this()
        {
            Name = name;
        }

        #endregion
    }
}