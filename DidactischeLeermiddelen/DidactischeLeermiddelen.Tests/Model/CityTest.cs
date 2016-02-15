using System;
using DidactischeLeermiddelen.Models.Domain.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model
{
    [TestClass]
    public class CityTest
    {
        private City testCity1;

        [TestMethod]
        public void ConstructorWithParamsCreatesCity()
        {
            City city1 = new City("Aalst","9300");
            Assert.AreEqual("aalst",city1.Name);
            Assert.AreEqual("9300", city1.PostalCode);
        }
        public void DefaultConstructorCreatesCity()
        {
            City testCity = new City();
            Assert.IsNotNull(testCity);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PostalCodeAndNameAreEmptyThrowsError()
        {
            City testCity = new City(string.Empty,string.Empty);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PostalCodeIsNullThrowsError()
        {
            testCity1 = new City();
            testCity1.Name = "Antwerpen";
            testCity1.PostalCode = null;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CityNameIsEmptyThrowsError()
        {
            testCity1 = new City();
            testCity1.Name = String.Empty;

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CityNameIsNullThrowsError()
        {
            testCity1 = new City();
            testCity1.Name = null;

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CityNameIs101LongThrowsError()
        {
            testCity1 = new City();
            testCity1.Name =
                "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij1";

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PostalCodeIs5LongThrowsError()
        {
            testCity1 = new City();
            testCity1.PostalCode = "12345";
        }
        [TestMethod]
        public void PostalCodeIs4LongCreatesIt()
        {
            testCity1 = new City();
            testCity1.PostalCode = "1234";
            Assert.AreEqual("1234", testCity1.PostalCode);

        }
        [TestMethod]
        public void CityNameIs5LongCreatesIt()
        {
            testCity1 = new City();
            testCity1.Name = "Aalst";
            Assert.AreEqual("aalst", testCity1.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CityNameIsOkButPostalCodeIsWhiteSpaceThrowsError()
        {
            testCity1 = new City("Aalst"," ");

        }
        [TestMethod]
        public void CitynameIs100LongCreatesIt()
        {
            testCity1 = new City("abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij","9400");
            Assert.AreEqual("abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij",testCity1.Name);
        }

    }
}
