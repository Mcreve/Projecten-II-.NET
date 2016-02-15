using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using DidactischeLeermiddelen.Models.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DidactischeLeermiddelen.Models.DAL
{
    /// <summary>
    /// Seeding the inital database
    /// </summary>
    /// <exception cref="DbEntityValidationException"></exception>
    public class LeermiddelenInitializer : DropCreateDatabaseAlways<LeermiddelenContext>
    {
        /// <summary>
        /// Seeding / filling the inital database with roles, customers and their Identity user profiles
        /// </summary>
        /// <exception cref="DbEntityValidationException"></exception>
        /// <param name="context"> Databank Context</param>
        protected override void Seed(LeermiddelenContext context)
        {
            base.Seed(context);
            try
            {
                CreateRoles(context);
                CreateCustomers(context);
            }
            catch (DbEntityValidationException e)
            {
                #region Log Error(s) of the database
                string s = "Error : Cannot create database...";
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
            #endregion
        }

        /// <summary>
        /// Creates a collection of customers, adds them to the Customer DbSet, 
        /// Creates Identity User accounts for them with a personal email adres and
        /// a global password: "Geen1id+".
        /// <see cref=CreateAccount""/>
        /// </summary>
        /// <param name="context"></param>
        private void CreateCustomers(LeermiddelenContext context)
        {
            #region In-Scope Variables
            Customer customer = null;
            ICollection<Customer> customers = new List<Customer>(); 
            #endregion

            #region Creating a collection of customers
            customer = new Customer
            {
                Name = "Didonato",
                FirstName = "Cody",
                Email = "Cody.Didonato@hogent.be",
            };
            customers.Add(customer);


            customer = new Customer
            {
                Name = "Fortenberry",
                FirstName = "Bailey",
                Email = "Bailey.Fortenberry@hogent.be",
            };
            customers.Add(customer);

            customer = new Customer
            {
                Name = "Hensley",
                FirstName = "Margarito",
                Email = "Margarito.Hensley@student.hogent.be",
            };
            customers.Add(customer);

            customer = new Customer
            {
                Name = "Piland",
                FirstName = "Rusty",
                Email = "Rusty.Piland@HOGENT.be",
            };
            customers.Add(customer);


            customer = new Customer
            {
                Name = "Voegele",
                FirstName = "Zack",
                Email = "Zack.Voegele@student.Hogent.be",
            };
            customers.Add(customer);

            customer = new Customer
            {
                Name = "Keirn",
                FirstName = "Tu",
                Email = "Tu.Keirn@Student.hogent.be",
            };
            customers.Add(customer);
            #endregion

            #region Add the collection to the DbSet
            context.Customers.AddRange(customers);
            context.SaveChanges();
            #endregion

            #region Creating the useraccounts for the customers
            foreach (Customer c in customers)
            {
                ApplicationUser account = new ApplicationUser
                {
                    UserName = c.Email,
                    Email = c.Email
                };
                CreateAccount(account, context);
            } 
            #endregion

            context.SaveChanges();
        }

        /// <summary>
        /// Creating the inital roles for the users
        /// </summary>
        /// <exception cref="DbEntityValidationException"></exception>
        /// <param name="context"> Databank Context</param>
        private void CreateRoles(LeermiddelenContext context)
        {
            #region In-Scope Variables
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            #endregion

            #region Roles
            #region Role 1 - Students
            var role = new IdentityRole("student");
            roleManager.Create(role);
            #endregion
            #region Role 2 - Lectors
            role = new IdentityRole("lector");
            roleManager.Create(role);
            context.SaveChanges();
            #endregion 
            #endregion
        }


        /// <summary>
        /// Creates the account of the customer
        /// </summary>
        /// <param name="user"></param>
        /// <param name="context"></param>
        /// <seealso cref="CreateCustomers"/>
        private void CreateAccount(ApplicationUser user, LeermiddelenContext context)
        {
            try
            {
                #region In-Scope Variables
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = user.GetRole(user.Email);
                #endregion

                #region Ask the UserManager to add them to the IdentityFramework
                userManager.Create(user, "Geen1id+");
                userManager.SetLockoutEnabled(user.Id, true);
                if (role != null)
                {
                    userManager.AddToRole(user.Id, role);
                }

                #endregion
            }
            catch (System.InvalidOperationException exception)
            #region Start Debugger
            {
                Debugger.Break();
                throw new InvalidOperationException(exception.Message);
            }
            catch (Exception exception)
            {
                Debugger.Break();
                throw new Exception(exception.Message);
            } 
            #endregion

        }
    }
}