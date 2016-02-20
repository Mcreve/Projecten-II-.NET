using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class LeermiddelenInitializer : DropCreateDatabaseAlways<LeermiddelenContext>
    {
        #region Methods

        /// <summary>
        ///     Initial seed of the database
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(LeermiddelenContext context)
        {
            this.context = context;
            userList = new UserRepository(context);
            //Accounts
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            //roles
            roleStore = new RoleStore<IdentityRole>(context);
            roleManager = new RoleManager<IdentityRole>(roleStore);

            try
            {
                CreateRoles();
                CreateUsers();
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

        #region Properties

        private LeermiddelenContext context;
        private UserRepository userList;

        private UserStore<ApplicationUser> userStore;
        private UserManager<ApplicationUser> userManager;

        private RoleStore<IdentityRole> roleStore;
        private RoleManager<IdentityRole> roleManager;

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
                var suffix = i > 5 ? "@hogent.be" : "@student.hogent.be";
                var email = initialFirstNames[i] + "." + initialLastNames[i] + suffix;
                var userType = UserFactory.DetermineUserTypeByEmailAddress(email);
                var user = UserFactory.CreateUser(userType);
                user.EmailAddress = email;
                user.FirstName = initialFirstNames[i];
                user.LastName = initialLastNames[i];
                userList.Add(user);

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

        #endregion
    }
}