using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class LearningUtilityDetails
    {
        #region Properties
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public bool Loanable { get; set; }
        public string ArticleNumber { get; set; }
        public virtual FieldOfStudy FieldOfStudy { get; set; }
        public virtual TargetGroup TargetGroup { get; set; }
        public virtual Company Company { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<LearningUtility> LearningUtilities { get; set; }
        #endregion

        #region Constructors
        public LearningUtilityDetails() { }
        #endregion
    }
}