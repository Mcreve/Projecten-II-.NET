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
        public IQueryable<LearningUtility> LearningUtilityList { get; set; }
        public User Lector1 { get; set; }
        public User Student1 { get; set; }
        public User Anonymous { get; set; }
        public Location Location1 { get; set; }
        public FieldOfStudy FieldOfStudy1 { get; set; }
        public Company Company1 { get; set; }
        public TargetGroup TargetGroup1 { get; set; }
        public LearningUtility LearningUtility1 { get; set; }
        public LearningUtility LearningUtility2 { get; set; }
        public LearningUtility LearningUtility3 { get; set; }
        public List<LearningUtility> LearningSearchResult { get; set; }
        public Reservation reservation1 { get; set; }
        public Reservation reservation2 { get; set; }

        public IQueryable<Reservation> reservationList { get; set; }

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
            createStudentReservation();
            createLectorReservation();
            CreateReservationList();

        }

        private void CreateLists()
        {
            LearningUtilityList = (new LearningUtility[]
            {
                LearningUtility1,
                LearningUtility2,
                LearningUtility3
            }).ToList().AsQueryable();
        }
        private IEnumerable<LearningUtility> getList()
        {
            return LearningUtilityList;
        }
        private void CreateTargetGroups()
        {
            TargetGroup1 = new TargetGroup { Id = 1, Name = "Kleuter" };
        }

        private void CreateCompanies()
        {
            Company1 = new Company { Id = 1, Name = "Hasbro" };
        }

        private void CreateLearningUtilities()
        {
            LearningUtility1 = new LearningUtility
            {
                Id = 1,
                Name = "Wereldbol",
                Description = "Wereldbol met tectonische platen",
                Company = Company1,
                ArticleNumber = "Art1001",
                Loanable = true,
                Location = Location1,
                Picture = @"\items\pictures\wereldbol.png",
                Price = 75m,
                AmountInCatalog = 6,
                AmountUnavailable = 0,


            };
            LearningUtility1.FieldsOfStudy.Add(FieldOfStudy1);
            LearningUtility1.TargetGroups.Add(TargetGroup1);


            LearningUtility2 = new LearningUtility
            {
                Id = 2,
                Name = "Dobbelsteen schatkist 162-delig",
                ArticleNumber = "MH1447",
                Company = Company1,
                Description = "Koffertje met verschillende soorten dobbelstenen: blanco, met cijfers,...",
                Loanable = true,
                Location = Location1,
                Picture = @"\items\pictures\dobbelsteen_schatkist_162-delig.jpg",
                Price = 35m,
                AmountInCatalog = 5,
                AmountUnavailable = 2
            };
            LearningUtility2.FieldsOfStudy.Add(FieldOfStudy1);
            LearningUtility2.TargetGroups.Add(TargetGroup1);
            LearningUtility3 = new LearningUtility
            {
                Id = 3,
                Name = "Rekenspelletjes optellen en aftrekken",
                ArticleNumber = "MX203510",
                Company = Company1,
                Description = "Spelbord op het opdrachtenboekje leggen > opdracht oplossen door het juiste cijfer van het spelbord op het juiste antwoord in het boekje te leggen > controle door het spelbord dicht te klappen en om te draaien > de patronen moeten overeen komen.",
                Loanable = false,
                Location = Location1,
                Picture = @"\items\pictures\rekenspelletjes_optellen_en_aftrekken.gif",
                Price = 10.9m,
                AmountInCatalog = 10,
                AmountUnavailable = 5

            };
            LearningUtility3.FieldsOfStudy.Add(FieldOfStudy1);
            LearningUtility3.TargetGroups.Add(TargetGroup1);

        }

        private void CreateFieldOfStudies()
        {
            FieldOfStudy1 = new FieldOfStudy { Id = 1, Name = "Aardrijkskunde" };
        }

        private void CreateLocations()
        {
            Location1 = new Location { Id = 1, Name = "GLEDE 1.011" };
        }

        private void CreateUsers()
        {
            Lector1 = UserFactory.CreateUserWithParameters("Sonja", "Vandermeersch", "Sonja.vandermeersch@hogent.be");
            Student1 = UserFactory.CreateUserWithParameters("Benjamin", "Vertonghen", "Benjamin.vertonghen@student.hogent.be");
        }

        private void createStudentReservation()
        {

            Student1.AddReservation(new DateTime(2016, 3, 22, 8, 30, 52), 3, LearningUtility1);


        }
        private void createLectorReservation()
        {

            Lector1.AddReservation(new DateTime(2016, 3, 22, 8, 30, 52), 3, LearningUtility1);
              
        
        }
        private void CreateReservationList()
        {
            reservationList = LearningUtility1.Reservations.AsQueryable();

        }
    }
}
