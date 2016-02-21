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
        [Display(Name = "Foto")]
        public string Picture { get; set; }
        [Display(Name = "Leermiddel")]
        public string Name { get; set; }
        [Display(Name = "Omschrijving")]
        public string Description { get; set; }
        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
        [Display(Name = "Uitleenbaar")]
        public bool Loanable { get; set; }
        public virtual FieldOfStudy FieldOfStudy { get; set; }
        public virtual TargetGroup TargetGroup { get; set; }
        public virtual Company Company { get; set; }
        [Display(Name = "Categorie")]
        public virtual Category Category { get; set; }
        [Display(Name = "Locatie")]
        public virtual Location Location { get; set; }
        public virtual ICollection<LearningUtility> LearningUtilities { get; set; }
        #endregion

        #region Constructors
        public LearningUtilityDetails() { }
        #endregion
    }
}