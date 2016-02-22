using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Properties;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class TargetGroup
    {
        #region Fields
        private string name;
        #endregion

        #region Properties
        public int Id { get; set; }
        /// <summary>
        /// Sets the name of the TargetGroup 
        /// Required: Min 1 Character, Max 100 Characters
        /// <exception cref="ValidationException"></exception>
        /// </summary>
        [DisplayName(@"Doelgroep")]
        [Required(ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "TargetGroupNameRegex")]
        [RegularExpression(@"(?i).{1,100}",
                  ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "TargetGroupNameRegex")]
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
        #endregion
        /// <summary>
        /// Default Constructor
        /// </summary>
        #region Constructors
        public TargetGroup()
        {

        }
        /// <summary>
        /// Constructor with 1 parameter, calls default constructor
        /// </summary>
        /// <param name="name"></param>
        public TargetGroup(string name) : this()
        {
            this.Name = name;
        }
        #endregion
    }
}