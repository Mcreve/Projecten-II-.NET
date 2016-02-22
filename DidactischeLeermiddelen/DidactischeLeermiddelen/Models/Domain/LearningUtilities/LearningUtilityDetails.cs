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
        [Required]
        public string Name { get; set; }
        [Display(Name = "Omschrijving")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "Onbekend")]
        public decimal? Price { get; set; }
        [Display(Name = "Uitleenbaar")]
        public bool Loanable { get; set; }
        [Display(Name = "Artikel Nr.")]
        public string ArticleNumber { get; set; }
        [Display(Name = "Leergebied")]
        public virtual FieldOfStudy FieldOfStudy { get; set; }
        [Display(Name = "Doelgroep")]
        public virtual TargetGroup TargetGroup { get; set; }
        [Display(Name = "Bedrijf")]
        public virtual Company Company { get; set; }
        [Display(Name = "Locatie")]
        [Required]
        public virtual Location Location { get; set; }
        public virtual ICollection<LearningUtility> LearningUtilities { get; set; }
        #endregion

        #region Constructors

        public LearningUtilityDetails()
        {
            LearningUtilities = new List<LearningUtility>();
            Loanable = true;
        }

        public LearningUtilityDetails(string name, string description, Location location):this()
        {
            Name = name;
            Description = description;
            Location = location;
        }

        #endregion
    }
}