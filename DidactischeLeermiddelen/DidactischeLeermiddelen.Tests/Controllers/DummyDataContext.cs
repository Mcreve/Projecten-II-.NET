using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Controllers
{
    class DummyDataContext
    {
        public IQueryable<LearningUtilityDetails> LearningUtilityDetailsList { get; set; }
        public User Lector1 { get; set; }
        public User Student1 { get; set; }
        public User Anonymous { get; set; }
        public Location Location1 { get; set; }
        public FieldOfStudy FieldOfStudy1 { get; set; }
        public Company Company1 { get; set; }
        public TargetGroup TargetGroup1 { get; set; }
        public LearningUtilityDetails LearningUtilityDetails1 { get; set; }
        public LearningUtilityDetails LearningUtilityDetails2 { get; set; }
        public LearningUtilityDetails LearningUtilityDetails3 { get; set; }
        public List<LearningUtilityDetails> LearningSearchResult { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DummyDataContext()
        {
            CreateUsers();
            CreateLocations();
            CreateFieldOfStudies();
            CreateCompanies();
            CreateTargetGroups();
            CreateLearningUtilities();
            CreateLists();
        }

        private void CreateLists()
        {
            LearningUtilityDetailsList = (new LearningUtilityDetails[]
            {
                LearningUtilityDetails1,
                LearningUtilityDetails2,
                LearningUtilityDetails3
            }).ToList().AsQueryable();
        }
       
        private void CreateTargetGroups()
        {
            TargetGroup1 = new TargetGroup("Kleuter");
        }

        private void CreateCompanies()
        {
            Company1 = new Company("Hasbro");
        }

        private void CreateLearningUtilities()
        {
            LearningUtilityDetails1 = new LearningUtilityDetails
            {
                Id = 1,
                Name = "Wereldbol",
                Description = "Wereldbol met tectonische platen",
                Company = Company1,
                ArticleNumber = "Art1001",
                Loanable = true,
                Location = Location1,
                Picture = @"\items\pictures\wereldbol.png",
                Price = 75m
            };
            LearningUtilityDetails1.FieldsOfStudy.Add(FieldOfStudy1);
            LearningUtilityDetails1.TargetGroups.Add(TargetGroup1);
            LearningUtilityDetails2 = new LearningUtilityDetails
            {
                Id = 2,
                Name = "Dobbelsteen schatkist 162-delig",
                ArticleNumber = "MH1447",
                Company = Company1,
                Description = "Koffertje met verschillende soorten dobbelstenen: blanco, met cijfers,...",
                Loanable = true,
                Location = Location1,
                Picture = @"\items\pictures\dobbelsteen_schatkist_162-delig.jpg",
                Price = 35m
            };
            LearningUtilityDetails1.FieldsOfStudy.Add(FieldOfStudy1);
            LearningUtilityDetails1.TargetGroups.Add(TargetGroup1);
            LearningUtilityDetails3 = new LearningUtilityDetails
            {
                Id = 3,
                Name = "Rekenspelletjes optellen en aftrekken",
                ArticleNumber = "MX203510",
                Company = Company1,
                Description = "Spelbord op het opdrachtenboekje leggen > opdracht oplossen door het juiste cijfer van het spelbord op het juiste antwoord in het boekje te leggen > controle door het spelbord dicht te klappen en om te draaien > de patronen moeten overeen komen.",
                Loanable = false,
                Location = Location1,
                Picture = @"\items\pictures\rekenspelletjes_optellen_en_aftrekken.gif",
                Price = 10.9m
            };
            LearningUtilityDetails1.FieldsOfStudy.Add(FieldOfStudy1);
            LearningUtilityDetails1.TargetGroups.Add(TargetGroup1);

        }

        private void CreateFieldOfStudies()
        {
            FieldOfStudy1 = new FieldOfStudy("Aardrijkskunde");
        }

        private void CreateLocations()
        {
            Location1 = new Location("GLEDE 1.011");
        }

        private void CreateUsers()
        {
            Lector1 = UserFactory.CreateUserWithParameters("Sonja", "Vandermeersch", "Sonja.vandermeersch@hogent.be");
            Student1 = UserFactory.CreateUserWithParameters("Benjamin", "Vertonghen", "Benjamin.vertonghen@student.hogent.be");
        }
    }
}
