using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.ViewModels
{
    public class LearningUtilityDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Afbeelding")]
        public string Picture { get; set; }
        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "Onbekend")]
        public decimal? Price { get; set; }
        [Display(Name = "Artikel Nr.")]
        public string ArticleNumber { get; set; }
        [Display(Name = "Doelgroep(en)")]
        public IEnumerable<string> TargetGroups { get; set; }
        [Display(Name = "Leergebied(en)")]
        public IEnumerable<string> FieldsOfStudy { get; set; }
        [Display(Name = "Totaal in catalogus")]
        public int AmountInCatalog { get; set; }
        [Display(Name = "Tijdelijk onbeschikbaar")]
        public int AmountUnavailable { get; set; }
        [Display(Name = "Omschrijving")]
        public string Description { get; set; }
        [Display(Name = "Bedrijf")]
        public string CompanyName { get; set; }
        [Display(Name = "Contactpersoon")]
        public string CompanyContactPersonName { get; set; }
        [Display(Name = "E-mail")]
        public string CompanyEmailAddress { get; set; }
        [Display(Name = "Website")]
        public string CompanyWebsite { get; set; }

        public LearningUtilityDetailsViewModel(LearningUtilityDetails learningUtilityDetails)
        {
            Id = learningUtilityDetails.Id;
            Description = learningUtilityDetails.Description;
            Name = learningUtilityDetails.Name;
            Picture = learningUtilityDetails.Picture;
            Price = learningUtilityDetails.Price;
            ArticleNumber = learningUtilityDetails.ArticleNumber;
            TargetGroups = learningUtilityDetails.TargetGroups.OrderBy(t => t.Name).Select(t => t.Name = t.Name);
            FieldsOfStudy = learningUtilityDetails.FieldsOfStudy.OrderBy(f => f.Name).Select(f => f.Name = f.Name);
            //AmountInCatalog = learningUtilityDetails.LearningUtilities.Count;
            CompanyName = learningUtilityDetails.Company.Name;
            CompanyContactPersonName = learningUtilityDetails.Company.ContactPersonName;
            CompanyEmailAddress = learningUtilityDetails.Company.EmailAddress;
            CompanyWebsite = learningUtilityDetails.Company.Website;
            // AmountUnavailable =

        }
    }
}