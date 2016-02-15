using System;
using System.Collections.Generic;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain;
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
        /// Name        :  'Tisch'
        /// Firstname   :  'Zoe'
        /// Email       :  'Zoe.Tisch@student.hognt.be'
        /// </summary>
        public Customer Tisch { get; private set; }
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
        /// Name        :  'Augustus'
        /// Firstname   :  'String.empty'
        /// Email       :  'Hisako.Augustus@Gmail.com'
        /// </summary>
        public Customer Augustus { get; private set; }
        /// <summary>
        /// Name        :  'Voegele'
        /// Firstname   :  'Zack'
        /// Email       :  'Zack.Voegele@student.Hogent.be'
        /// </summary>
        public Customer Voegele { get; private set; }
        /// <summary>
        /// Name        :  'NULL'
        /// Firstname   :  'Ramon'
        /// Email       :  'Ramon@Student.Hogent.be'
        /// </summary>
        public Customer Bennefield { get; private set; }
        /// <summary>
        /// Name        :  'Keirn'
        /// Firstname   :  'Tu'
        /// Email       :  'String.empty'
        /// </summary>
        public Customer Keirn { get; private set; }
        /// <summary>
        /// Name        :  'Vertonghen'
        /// Firstname   :  'Benjamin'
        /// Email       :  'Benjamin.vertonghen@student.hogent.be'
        /// </summary>
        public Customer Vertonghen { get; private set; }
        /// <summary>
        /// Name        :  'De bruyn'
        /// Firstname   :  'Helleni'
        /// Email       :  'String.empty'
        /// </summary>
        public Customer DeBruyn { get; private set; }
        /// <summary>
        ///     Name        : 'String.empty'
        ///  |  Firstname   : 'Kevin'
        ///  |  Email       : 'Kevin@hogent.be'
        /// </summary>
        public Customer VanLeuven { get; private set; }
        /// <summary>
        ///     Name        : 'De Ridder'
        ///  |  Firstname   : 'Ingrid'
        ///  |  Email       : 'Ingrid.DeRidder@ogent.be'
        /// </summary>
        public Customer DeRidder { get; private set; }
        public DummyDataContext()
        {
            
            #region Creating a collection of customers
            Didonato = new Customer
            {
                Name = "Didonato",
                FirstName = "Cody",
                Email = "Cody.Didonato@HoGent.Be",
            };

            Tisch = new Customer
            {
                Name = "Tisch",
                FirstName = "Zoe",
                Email = "Zoe.Tisch@student.hognt.be",
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

            Augustus = new Customer
            {
                Name = "Augustus",
                FirstName = string.Empty,
                Email = "Hisako.Augustus@Gmail.com",
            };

            Voegele = new Customer
            {
                Name = "Voegele",
                FirstName = "Zack",
                Email = "Zack.Voegele@student.Hogent.be",
            };

            Bennefield = new Customer
            {
                Name = null,
                FirstName = "Ramon",
                Email = "Ramon@Student.Hogent.be",
            };

            Keirn = new Customer
            {
                Name = "Keirn",
                FirstName = "Tu",
                Email = string.Empty,
            };

            Vertonghen = new Customer
            {
                Name = "Vertonghen",
                FirstName = "Benjamin",
                Email = "benjamin.vertonghen@student.hogent.be",
            };

            DeBruyn = new Customer
            {
                Name = "De Bruyn",
                FirstName = "Helleni",
                Email = null,
            };

            VanLeuven = new Customer
            {
                Name = string.Empty,
                FirstName = "Kevin",
                Email = "Kevin@hogent.be",
            };

            DeRidder = new Customer
            {
                Name = "De Ridder",
                FirstName = "Ingrid",
                Email = "Ingrid.DeRidder@ogent.be",
            };

            #endregion

        }
    }
}
