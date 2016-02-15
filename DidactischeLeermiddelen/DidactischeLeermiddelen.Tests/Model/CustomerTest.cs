using System;
using DidactischeLeermiddelen.Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model
{
    [TestClass]
    public class CustomerTest
    {
        Customer initialCustomer;

        [TestInitialize]
        public void CustomerTestInitialize()
        {
            initialCustomer = new Customer("Vertonghen",
                                    "Benjamin",
                                    "ben.vertonghen@student.hogent.be");
        }

        [TestMethod]
        public void ConstructorWithParametersNameFirstNameEmail()
        {
            Assert.AreEqual("vertonghen", initialCustomer.Name);
            Assert.AreEqual("benjamin", initialCustomer.FirstName);
            Assert.AreEqual("ben.vertonghen@student.hogent.be", initialCustomer.Email);
        }
        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]
        public void EmailIsEmpty()
        {
            Customer customer1 = new Customer();
            customer1.Name = "De Bruyn";
            customer1.FirstName = "Helleni";
            customer1.Email = " ";
            customer1.Email = String.Empty;
        }

        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]
        public void EmailIsNull()
        {
            Customer customer1 = new Customer();
            customer1.Name = "De Bruyn";
            customer1.FirstName = "Helleni";
            customer1.Email = null;
        }
        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]
        public void NameIsEmpty()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@hotmail.com";
            customer1.FirstName = "Helleni";
            customer1.Name = " ";
            customer1.Name = String.Empty;
        }

        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]
        public void NameIsNull()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@hotmail.com";
            customer1.FirstName = "Helleni";
            customer1.Name = null;
        }
        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]
        public void FirstNameIsEmpty()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@hotmail.com";
            customer1.Name = "De Bruyn";
            customer1.FirstName = " ";
            customer1.FirstName = String.Empty;
        }

        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]
        public void FirstNameIsNull()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@hotmail.com";
            customer1.Name = "De Bruyn";
            customer1.FirstName = null;
        }

        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]
        public void EmailIsNotFromHoGent()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@hotmail.com";
        }
        [TestMethod]
        public void EmailIsStudentFromHoGentLowerCase()
        {
            Customer customer1 = new Customer();
            customer1.Email = "de.bruyn@student.hogent.be";
            Assert.AreEqual("de.bruyn@student.hogent.be", customer1.Email);

        }
        [TestMethod]
        public void EmailIsLectorFromHoGentLowerCase()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@hogent.be";
            Assert.AreEqual("de.bruyn@hogent.be", customer1.Email);
        }
        [TestMethod]
        public void EmailIsStudentFromHoGentCamelCase()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@StuDenT.HogeNt.bE";
            Assert.AreEqual("de.bruyn@student.hogent.be", customer1.Email);

        }
        [TestMethod]
        public void EmailIsLectorFromHoGentCamelCase()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@HoGenT.Be";
            Assert.AreEqual("de.bruyn@hogent.be", customer1.Email);
        }
        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]
        public void EmailIsStudentFromHoGentButMisspelled()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@StudenT.Hoget.bE";

        }
        [TestMethod]
        [ExpectedException((typeof(ArgumentException)))]

        public void EmailIsLectorFromHoGentCamelCaseButMisspelled()
        {
            Customer customer1 = new Customer();
            customer1.Email = "De.Bruyn@HoenT.Be";
        }
    }
}