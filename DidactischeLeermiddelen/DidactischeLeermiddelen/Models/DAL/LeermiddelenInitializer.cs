using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Enums;
using DidactischeLeermiddelen.Models.Domain.Locations;
using DidactischeLeermiddelen.Models.Domain.Products;
using DidactischeLeermiddelen.Models.Domain.Users;
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
        private LeermiddelenContext context;
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
                this.context = context;

                CreateRoles();
                CreateCustomers();
                CreateCategories();
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

    

        private void CreateCategories()
        {
            City Aalst = new City("Aalst", "9300");
            City Gent = new City("Gent", "9000");


            Classroom classroom1 = new Classroom("be879");
            Classroom classroom2 = new Classroom("classroom...");

            Location CampusAalst = new Location("Campus Aalst", "Nieuwstraat", "3", Aalst);
            Location CampusSchoonmeersen = new Location("Campus Schoonmeersen", "Groenstraat", "4563", Gent);


            CampusAalst.AddClassroom(classroom1);
            CampusSchoonmeersen.AddClassroom(classroom2);

            Product Product1 = new Product { Location = CampusAalst };
            Product Product2 = new Product { Location = CampusSchoonmeersen, Availability = Availability.Blocked };
            Product Product3 = new Product { Location = CampusSchoonmeersen, Availability = Availability.Reserved };
            Product Product4 = new Product { Location = CampusSchoonmeersen, Availability = Availability.Available };
            Product Product5 = new Product { Location = CampusAalst, Availability = Availability.Available };
            Product Product6 = new Product { Location = CampusSchoonmeersen, Availability = Availability.Unavailable };
            Product Product7 = new Product { Location = CampusAalst, Availability = Availability.Blocked };
            Product Product8 = new Product { Location = CampusSchoonmeersen, Availability = Availability.Unavailable };
            Product Product9 = new Product { Location = CampusAalst, Availability = Availability.Reserved };
            Product Product10 = new Product { Location = CampusSchoonmeersen, Availability = Availability.Reserved };
            Product Product11 = new Product { Location = CampusAalst, Availability = Availability.Unavailable };
            Product Product12 = new Product { Location = CampusSchoonmeersen, Availability = Availability.Unavailable };


            ProductGroup ProductGroup1 = new ProductGroup("Skelet Aap",
                          "Een skelet van een Aap",
                          255M, Loanable.Loanable, string.Empty);

            ProductGroup ProductGroup2 = new ProductGroup("Wereldkaart",
                                             "Een wereldkaart",
                                             25.5M, Loanable.Loanable, string.Empty);

            ProductGroup ProductGroup3 = new ProductGroup("Film van geschiedenis",
                                 "Een film over...", 25M, Loanable.UnLoanable, string.Empty);

            ProductGroup ProductGroup4 = new ProductGroup("Map Leonardo Da Vinci",
                                 "Een schatkaart van Leonardo", 2.5M, Loanable.UnLoanable, string.Empty);
            ProductGroup1.AddProduct(Product1);
            ProductGroup1.AddProduct(Product2);

            ProductGroup2.AddProduct(Product3);
            ProductGroup2.AddProduct(Product4);
            ProductGroup2.AddProduct(Product5);
            ProductGroup2.AddProduct(Product6);


            ProductGroup3.AddProduct(Product7);
            ProductGroup3.AddProduct(Product8);
            ProductGroup3.AddProduct(Product9);
            ProductGroup3.AddProduct(Product10);

            ProductGroup4.AddProduct(Product11);
            ProductGroup4.AddProduct(Product12);

            Category Category1 = new Category("Biologie");
            Category Category2 = new Category("Geografie");
            Category Category3 = new Category("Geschiedenis");

            Category1.AddProductGroup(ProductGroup1);
            Category2.AddProductGroup(ProductGroup2);
            Category3.AddProductGroup(ProductGroup3);
            Category3.AddProductGroup(ProductGroup4);


            context.Products.AddRange(new Product[] { Product1, Product2, Product3, Product4 });
            context.ProductGroups.AddRange(new ProductGroup[] { ProductGroup1, ProductGroup2, ProductGroup3 });
            context.Categories.AddRange(new Category[] { Category1, Category2, Category3 });
            context.SaveChanges();

        }


        /// <summary>
        /// Creates a collection of customers, adds them to the Customer DbSet, 
        /// Creates Identity User accounts for them with a personal email adres and
        /// a global password: "Geen1id+".
        /// <see cref=CreateAccount""/>
        /// </summary>
        /// <param name="context"></param>
        private void CreateCustomers()
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
                Name = "Tisch",
                FirstName = "Zoe",
                Email = "Zoe.Tisch@student.hoent.be",
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
                Name = "Augustus",
                FirstName = "Hisako",
                Email = "Hisako.Augustus@GMAIL.com",
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
                Name = "Bennefield",
                FirstName = "Ramon",
                Email = "Ramon.Bennefield@Gmail.com",
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
                CreateAccount(account);
            } 
            #endregion

            context.SaveChanges();
        }

        /// <summary>
        /// Creating the inital roles for the users
        /// </summary>
        /// <exception cref="DbEntityValidationException"></exception>
        /// <param name="context"> Databank Context</param>
        private void CreateRoles()
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
        private void CreateAccount(ApplicationUser user)
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