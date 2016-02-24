using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class LeermiddelenInitializer : DropCreateDatabaseAlways<LeermiddelenContext>
    {        
        #region Properties
        private LeermiddelenContext context;
        private ICollection<Location> locations;
        private UserStore<ApplicationUser> userStore;
        private UserManager<ApplicationUser> userManager;
        private RoleStore<IdentityRole> roleStore;
        private RoleManager<IdentityRole> roleManager;
        private ICollection<User> users;
        private ICollection<Company> companies;
        private ICollection<FieldOfStudy> fieldsOfStudy;
        private ICollection<TargetGroup> targetGroups;
        #endregion

        #region Methods
        /// <summary>
        /// Initial seed of the database. Add a private method in the "try - catch" block to add additional data to the database.
        /// </summary>
        /// <param name="context">The context instance that should be seeded</param>
        protected override void Seed(LeermiddelenContext context)
        {
            this.context = context;
            //Accounts
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            //roles
            roleStore = new RoleStore<IdentityRole>(context);
            roleManager = new RoleManager<IdentityRole>(roleStore);
            //Locations
            locations = new List<Location>();
            users = new List<User>();

            try
            {
                CreateRoles();
                CreateUsers();
                CreateLocations();
                CreateLearningUtilityDetails();
            }
            catch (DbEntityValidationException e)
            {
                var s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }
        #endregion

        #region Private methods

        #region Users creation
        /// <summary>
        /// Creates 5 Student users and accounts, Creates 5 Lector users and accounts
        /// </summary>
        private void CreateUsers()
        {

            string[] initialFirstNames =
            {
                "Benjamin", "Jan", "Maxim", "Ward", "Ingeborg", "Sonja", "Mark", "Petra",
                "Els", "Carl"
            };
            string[] initialLastNames =
            {
                "Vertonghen", "Marien", "Hupeldepup", "Vanlerberghe", "Vermassen", "Brouwer",
                "Verstraten", "Schoeikens", "Verhoeven", "Merkx"
            };
            //create 5 students and 5 lectors
            for (var i = 0; i < 10; i++)
            {
                string suffix = i > 5 ? "@hogent.be" : "@student.hogent.be";
                string firstName = initialFirstNames[i];
                string lastName = initialLastNames[i];
                string email = initialFirstNames[i] + "." + initialLastNames[i] + suffix;

                var user = UserFactory.CreateUserWithParameters(firstName, lastName, email);
                context.UserList.Add(user);
                users.Add(user);
                CreateAccount(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Creates the account for the user and sets the role for the user depending on
        /// it's emailaddres suffix.
        /// </summary>
        /// <param name="user">The user for wich the account should be created in the database</param>
        private void CreateAccount(User user)
        {
            var account = new ApplicationUser
            {
                UserName = user.EmailAddress,
                Email = user.EmailAddress
            };
            var userType = UserFactory.DetermineUserTypeByEmailAddress(user.EmailAddress);

            var role = roleManager.FindByName(userType.ToString());

            userManager.Create(account, "Geen1id+");
            userManager.SetLockoutEnabled(account.Id, false);
            userManager.AddToRole(account.Id, role.Name);
        }

        /// <summary>
        /// Creates the roles of the application: Student and Lector
        /// </summary>
        private void CreateRoles()
        {
            //Create the role for students
            var studentRole = new IdentityRole(UserType.Student.ToString());
            roleManager.Create(studentRole);

            //Create the role for lectors
            var lectorRole = new IdentityRole(UserType.Lector.ToString());
            roleManager.Create(lectorRole);
            context.SaveChanges();
        } 
        #endregion
        
        #region LearningUtility creation
        /// <summary>
        /// Creates the initial locations and adds them to a list.
        /// </summary>
        private void CreateLocations()
        {
            string[] initialLocationNames = { "GLEDE 1.011", "Biolabo kast 1 &2 ", "Preparatie ", "Biolabo kast 3 ", "Biolabo kast 5 ", "Biolabo kast 2 " };
            foreach (var initialLocationName in initialLocationNames)
            {
                var location = new Location(initialLocationName);
                locations.Add(location);
            }
        }

        /// <summary>
        /// This method creates some FieldOfStudy objects and adds them to the fieldsOfStudy field.
        /// </summary>
        private void CreateFieldsOfStudy()
        {
            fieldsOfStudy.Add(new FieldOfStudy("Aarderijkskunde"));
            fieldsOfStudy.Add(new FieldOfStudy("Ontspanning"));
            fieldsOfStudy.Add(new FieldOfStudy("Wiskunde"));
        }

        /// <summary>
        /// This method creates some Company objects and adds them to the companies field.
        /// </summary>
        private void CreateCompanies()
        {
            companies.Add(new Company("Wolters"));
            companies.Add(new Company("Hasbro"));
            companies.Add(new Company("Texas Instruments"));
        }

        /// <summary>
        /// This method creates some TargetGroup objects and adds them to the targetGroups field.
        /// </summary>
        private void CreateTargetGroups()
        {
            targetGroups.Add(new TargetGroup("6-12 jaar"));
            targetGroups.Add(new TargetGroup("7-9 jaar"));
            targetGroups.Add(new TargetGroup("Tweede leerjaar"));
        }

        /// <summary>
        /// This method creates a new LearningUtility to be added to a learningUtilityDetails object. This method sets
        /// the initial state and the properties ReservedBy and LendTo of the new instance.
        /// </summary>
        /// <param name="stateType">The initial state the learningUtility should be in</param>
        /// <param name="reservedBy">The user that the item is reserved to</param>
        /// <param name="lendTo">The user that the item is lend to</param>
        /// <returns>A LearningUtility instance</returns>
        private LearningUtility CreateLearningUtility(StateType stateType, User reservedBy, User lendTo)
        {
            LearningUtility learningUtility = new LearningUtility
            {
                ReservedBy = stateType == StateType.Unavailable || stateType == StateType.Available ? null : reservedBy,
                LendTo = stateType == StateType.Late || stateType == StateType.HandedOut ? lendTo : null
            };
            learningUtility.ToState(StateFactory.CreateState(stateType, learningUtility));
            return learningUtility;
        }


        /// <summary>
        /// This method creates some LearningUtilityDetails objects to seed the database
        /// </summary>
        private void CreateLearningUtilityDetails()
        {
            CreateCompanies();
            CreateFieldsOfStudy();
            CreateTargetGroups();
            //Create worldglobe object
            LearningUtilityDetails learningUtilityDetails = new LearningUtilityDetails
            {
                Name = "Wereldbol",
                Description = "Wereldbol",
                Company = companies.First(c => c.Name.Equals("Wolters")),
                FieldOfStudy = fieldsOfStudy.First(f => f.Name.Equals("Aarderijkskunde")),
                ArticleNumber = "Art1001",
                Loanable = true,
                Location = locations.First(),
                TargetGroup = targetGroups.First(t => t.Name.Equals("6-12 jaar")),
                Picture = @"\items\pictures\wereldbol.jpg",
                Price = 75m
            };
            learningUtilityDetails.LearningUtilities.Add(CreateLearningUtility(StateType.Available, null, null));
            learningUtilityDetails.LearningUtilities.Add(CreateLearningUtility(StateType.Reserved, users.First(), null));
            context.LearningUtilityDetailsList.Add(learningUtilityDetails);

            //Create dobbelsteenschatkist object
            learningUtilityDetails = new LearningUtilityDetails
            {
                Name = "Dobbelsteen schatkist 162-delig",
                ArticleNumber = "MH1447",
                Company = companies.First(c => c.Name.Equals("Hasbro")),
                Description = "Koffertje met verschillende soorten dobbelstenen: blanco, met cijfers,...",
                FieldOfStudy = fieldsOfStudy.First(f => f.Name.Equals("Ontspanning")),
                Loanable = true,
                Location = locations.First(),
                Picture = @"\items\pictures\dobbelsteen_schatkist_162-delig.jpg",
                Price = 35m,
                TargetGroup = targetGroups.First(t => t.Name.Equals("7-9 jaar"))
            };
            learningUtilityDetails.LearningUtilities.Add(CreateLearningUtility(StateType.HandedOut, null, users.ElementAtOrDefault(1)));
            learningUtilityDetails.LearningUtilities.Add(CreateLearningUtility(StateType.Unavailable, null, null));
            context.LearningUtilityDetailsList.Add(learningUtilityDetails);

            //Create rekenspelletjes object
            learningUtilityDetails = new LearningUtilityDetails
            {
                Name = "Rekenspelletjes optellen en aftrekken",
                ArticleNumber = "MX203510",
                Company = companies.First(c => c.Name.Contains("Texas")),
                Description = "Spelbord op het opdrachtenboekje leggen > opdracht oplossen door het juiste cijfer van het spelbord op het juiste antwoord in het boekje te leggen > controle door het spelbord dicht te klappen en om te draaien > de patronen moeten overeen komen.",
                FieldOfStudy = fieldsOfStudy.First(f => f.Name.Equals("Wiskunde")),
                Loanable = false,
                Location = locations.ElementAtOrDefault(1),
                Picture = @"\items\pictures\rekenspelletjes_optellen_en_aftrekken.jpg",
                Price = 10.9m,
                TargetGroup = targetGroups.First(t => t.Name.Contains("leerjaar"))
            };
            learningUtilityDetails.LearningUtilities.Add(CreateLearningUtility(StateType.Blocked, users.ElementAtOrDefault(5), null));
            context.LearningUtilityDetailsList.Add(learningUtilityDetails);

            context.SaveChanges();
        } 
        #endregion

        #endregion
    }
}