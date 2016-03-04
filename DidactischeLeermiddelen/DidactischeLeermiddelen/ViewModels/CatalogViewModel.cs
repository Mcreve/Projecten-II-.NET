﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models
{
    public class CatalogViewModel
    {

        [Display(Name = "Omschrijving")]
        public string ShortDescription { get; set; }
        public int Id { get; set; }
        [Display(Name = "Leermiddel")]
        public string Name { get; set; }
        [Display(Name = "Afbeelding")]
        public string Picture { get; set; }
        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "Onbekend")]
        public decimal? Price { get; set; }
        [Display(Name = "Artikel Nr.")]
        public string ArticleNumber { get; set; }
        [Display(Name = "Beschikbaar")]
        public int AmountInStock { get; set; }
        [Display(Name = "Doelgroep")]
        public string TargetGroup { get; set; }
        [Display(Name = "Leergebied")]
        public string FieldOfStudy { get; set; }
        [Display(Name = "Totaal")]
        public int AmountInCatalog { get; set; }
        [Display(Name = "Tijdelijk onbeschikbaar")]
        public int AmountUnavailable { get; set; }
        [Display(Name = "Gereserveerd")]
        public int AmountBlocked { get; set; }

        public CatalogViewModel()
        {
            
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="learningUtilityDetails"></param>
        public CatalogViewModel(LearningUtilityDetails learningUtilityDetails)
        {
            Id = learningUtilityDetails.Id;
            ShortDescription = ConvertToShortDescription(learningUtilityDetails.Description);
            Name = learningUtilityDetails.Name;
            Picture = learningUtilityDetails.Picture;
            Price = learningUtilityDetails.Price;
            ArticleNumber = learningUtilityDetails.ArticleNumber;
           // AmountInStock =
               
            TargetGroup = null;
            FieldOfStudy = null;
           // AmountBlocked =
             
           // AmountUnavailable =
               
        }
        /// <summary>
        /// Returns the shortdescription of the LearningUtilityDetail, the first 250 characters.
        /// </summary>
        /// <param name="description"></param>
        /// <returns>First 250 Charcters of the Description</returns>
        private string ConvertToShortDescription(string description)
        {
            if (description.Length > 250)
            {
                return description.Substring(0, 247) + "...";
            }
            return description;
        }
    }
}