using System;
using System.Collections;
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
        private UserRepository userList;
        private LearningUtilityDetailsRepository learningUtilityDetailsList;
        private IList<Location> locations;

        private UserStore<ApplicationUser> userStore;
        private UserManager<ApplicationUser> userManager;

        private RoleStore<IdentityRole> roleStore;
        private RoleManager<IdentityRole> roleManager;
        private ICollection<User> users;

        #endregion

        #region Methods

        /// <summary>
        ///     Initial seed of the database
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(LeermiddelenContext context)
        {
            this.context = context;
            userList = new UserRepository(context);
            learningUtilityDetailsList = new LearningUtilityDetailsRepository(context);
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
                CreateLearningUtilities();
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

        /// <summary>
        ///     Creates 5 Student users and accounts, Creates 5 Lector users and accounts
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
        
                var user = UserFactory.CreateUserWithParameters(firstName,lastName,email);
                userList.Add(user);
                users.Add(user);
                CreateAccount(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        ///     Creates the account for the user
        /// </summary>
        /// <param name="user"></param>
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
        ///     Creates the roles of the application
        ///     - Student
        ///     - Lector
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

        private void CreateLearningUtilities()
        {
            LearningUtility learningUtility;
            Boolean flag = true;
            int count = 0;
            foreach(User user in users)
            {
                learningUtility = new LearningUtility();
                if (flag)
                {
                    learningUtility.LendTo = user;
                    learningUtility.ReservedBy = user;
                    learningUtility.ToState(StateFactory.CreateState(StateType.HandedOut, learningUtility));
                    flag = false;
                } else
                {
                    learningUtility.ToState(StateFactory.CreateState(StateType.Available, learningUtility));
                    flag = true;
                }
                CreateLearningUtilityDetails(learningUtility, ++count);
            }
        }

        private void CreateLearningUtilityDetails(LearningUtility learningUtility, int suffix)
        {
            LearningUtilityDetails learningUtilityDetails = new LearningUtilityDetails
            {
                Name = "Wereldbol" + suffix,
                Description = "Wereldbol " + suffix,
                Company = new Company { Name = "Company" + suffix },
                FieldOfStudy = new FieldOfStudy { Name = "FieldOfStudy" + suffix },
                ArticleNumber = "Art100" + suffix,
                Loanable = suffix % 2 == 0 ? true : false,
                Location = locations.First(),
                TargetGroup = new TargetGroup { Name = "Leerjaar " + suffix }
            };
            learningUtilityDetails.LearningUtilities.Add(learningUtility);
            context.LearningUtilityDetailsList.Add(learningUtilityDetails);
            context.SaveChanges();
        }
        #endregion
    }
}