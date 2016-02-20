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
        private LeermiddelenContext context;
        private UserRepository userList;

        private UserStore<ApplicationUser> userStore;
        private UserManager<ApplicationUser> userManager; 

        private RoleStore<IdentityRole> roleStore;
        private RoleManager<IdentityRole> roleManager;


        protected override void Seed(LeermiddelenContext context)
        {
            this.context = context;
            this.userList = new UserRepository(context);
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
                string s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                         eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }

        }

        private void CreateUsers()
        {
            string[] initialFirstNames = new string[] { "Benjamin", "Jan", "Maxim", "Ward", "Ingeborg","Sonja","Mark","Petra","Els","Carl" };
            string[] initialLastNames = new string[] {"Vertonghen", "Marien", "Hupeldepup", "Vanlerberghe", "Vermassen","Brouwer", "Verstraten","Schoeikens","Verhoeven","Merkx"};
            //create 5 students and 5 lectors
            for (int i = 0; i <9 ; i++)
            {
                string suffix = i > 5 ? "@hogent.be" : "@student.hogent.be";
                string email = initialFirstNames[i] + "." + initialLastNames[i] + suffix;
                UserType userType = UserFactory.DetermineUserTypeByEmailAddress(email);
                User user = UserFactory.CreateUser(userType);
                user.EmailAddress = email;
                user.FirstName = initialFirstNames[i];
                user.LastName = initialLastNames[i];
                userList.Add(user);

                CreateAccount(user);
                context.SaveChanges();
            }
        }

        private void CreateAccount(User user)
        {
            ApplicationUser account = new ApplicationUser()
            {
                UserName = user.EmailAddress,
                Email = user.EmailAddress
            };
            UserType userType = UserFactory.DetermineUserTypeByEmailAddress(user.EmailAddress);

            var role = roleManager.FindByName(userType.ToString());

            userManager.Create(account, "Geen1id+");
            userManager.SetLockoutEnabled(account.Id, false);
            userManager.AddToRole(account.Id, role.Name);
        }

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
    }
}