using System;
using System.Collections.Generic;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Enums;
using DidactischeLeermiddelen.Models.Domain.Locations;
using DidactischeLeermiddelen.Models.Domain.Products;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Controllers
{
    public class DummyDataContext
    {
        /// <summary>
        /// Name        :  'Didonato'
        /// Firstname   :  'Cody'
        /// Email       :  'Cody.Didonato@HoGent.Be'
        /// </summary>
        public Customer Didonato { get; private set; }
  
        /// <summary>
        /// Name        :  'Fortenberry'
        /// Firstname   :  'Bailey'
        /// Email       :  'Bailey.Fortenberry@hogent.be'
        /// </summary>
        public Customer Fortenberry { get; private set; }
        /// <summary>
        /// Name        :  'Margarito'
        /// Firstname   :  'Hensley'
        /// Email       :  'Margarito.Hensley@student.hogent.be'
        /// </summary>
        public Customer Hensley { get; private set; }
        /// <summary>
        /// Name        :  'Piland'
        /// Firstname   :  'Rusty'
        /// Email       :  'Rusty.Piland@HOGENT.be'
        /// </summary>
        public Customer Piland { get; private set; }
        /// <summary>
        /// Name        :  'Voegele'
        /// Firstname   :  'Zack'
        /// Email       :  'Zack.Voegele@student.Hogent.be'
        /// </summary>
        public Customer Voegele { get; private set; }
        /// <summary>
        /// Name        :  'Vertonghen'
        /// Firstname   :  'Benjamin'
        /// Email       :  'Benjamin.vertonghen@student.hogent.be'
        /// </summary>
        public Customer Vertonghen { get; private set; }

        public Product Product1 { get; set; }
        public Product Product2 { get; set; }
        public Product Product3 { get; set; }
        public Product Product4 { get; set; }
        public Location CampusSchoonmeersen { get; set; }
        public Location CampusAalst { get; set; }
        public City Gent { get; set; }
        public City Aalst { get; set; }
        public Category Category1 { get; set; }
        public Category Category2 { get; set; }
        public Category Category3 { get; set; }
        public ProductGroup ProductGroup1 { get; set; }
        public ProductGroup ProductGroup2 { get; set; }
        public ProductGroup ProductGroup3 { get; set; }

        public DummyDataContext()
        {
            CreateCustomers();
            CreateCities();
            CreateLocations();
            CreateProducts();
            CreateProductGroups();
            CreateCategories();


        }

        private void CreateCustomers()
        {
            #region Creating a collection of customers

            Didonato = new Customer
            {
                Name = "Didonato",
                FirstName = "Cody",
                Email = "Cody.Didonato@HoGent.Be",
            };

            Fortenberry = new Customer
            {
                Name = "Fortenberry",
                FirstName = "Bailey",
                Email = "Bailey.Fortenberry@hogent.be",
            };

            Hensley = new Customer
            {
                Name = "Hensley",
                FirstName = "Margarito",
                Email = "Margarito.Hensley@student.hogent.be",
            };

            Piland = new Customer
            {
                Name = "Piland",
                FirstName = "Rusty",
                Email = "Rusty.Piland@HOGENT.be",
            };


            Voegele = new Customer
            {
                Name = "Voegele",
                FirstName = "Zack",
                Email = "Zack.Voegele@student.Hogent.be",
            };

            Vertonghen = new Customer
            {
                Name = "Vertonghen",
                FirstName = "Benjamin",
                Email = "benjamin.vertonghen@student.hogent.be",
            };

            #endregion
        }
        private void CreateCities()
        {
            Aalst = new City("Aalst","9300");
            Gent = new City("Gent","9000");
        }
        private void CreateLocations()
        {
            CampusAalst = new Location("Campus Aalst","Nieuwstraat","3",Aalst);
            CampusSchoonmeersen = new Location("Campus Schoonmeersen", "Groenstraat", "4563", Gent);



        }
        private void CreateProducts()
        {
            Product1 = new Product {Location = CampusAalst};
            Product2 = new Product { Location = CampusSchoonmeersen,Availability = Availability.Blocked};
            Product3 = new Product { Location = CampusSchoonmeersen, Availability = Availability.Reserved };
            Product4 = new Product { Location = CampusAalst, Availability = Availability.Unavailable };
        }

        private void CreateProductGroups()
        {
            ProductGroup1 = new ProductGroup("Skelet Aap", "Een skelet van een Aap", 255M, Loanable.Loanable,
                string.Empty);
            ProductGroup1.AddProduct(Product1);
            ProductGroup1.AddProduct(Product2);
            
            ProductGroup2 = new ProductGroup("Wereldkaart", "Een wereldkaart", 25.5M, Loanable.Loanable, string.Empty);
            ProductGroup2.AddProduct(Product1);
            ProductGroup2.AddProduct(Product1);
            ProductGroup2.AddProduct(Product1);
            ProductGroup2.AddProduct(Product1);
            ProductGroup3 = new ProductGroup("Film van geschiedenis", "Een film over...", 25M, Loanable.UnLoanable,
                string.Empty);
            ProductGroup3.AddProduct(Product2);
            ProductGroup3.AddProduct(Product4);
            ProductGroup3.AddProduct(Product2);
            ProductGroup3.AddProduct(Product1);
        }
        private void CreateCategories()
        {
            Category1 = new Category("Biologie");
            Category1.AddProductGroup(ProductGroup1);

            Category2 = new Category("Geografie");
            Category2.AddProductGroup(ProductGroup2);
            
            Category3 = new Category("Geschiedenis");
            Category3.AddProductGroup(ProductGroup3);
        }
    }
}
