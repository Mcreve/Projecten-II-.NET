using System;
using DidactischeLeermiddelen.Models.Domain.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model
{
    [TestClass]
    public class LocationTest
    {
        private Location testLocation;
        private City city1;
        private Classroom classroom1;
        private Classroom classroom2;

        [TestInitialize]
        public void LocationTestInitializer()
        {
            city1 = new City("Gent", "9000");
            classroom1 = new Classroom("be54");
            classroom2 = new Classroom("het hiologie lokaal");

        }

        [TestMethod]
        public void ConstructorWithParamsCreatesLocation()
        {
            Location location = new Location("Campus Schoonmeersen","TestStraat","154",city1);
            Assert.AreEqual("campus schoonmeersen", location.Name);
            Assert.AreEqual("teststraat", location.Street);
            Assert.AreEqual("154", location.HouseNumber);
            Assert.AreEqual("gent", location.City.Name);
            Assert.AreEqual("9000", location.City.PostalCode);
            Assert.IsNotNull(location.Classrooms);

        }
        public void DefaultConstructorCreatesLocationWithClassrooms()
        {
            Location location = new Location();
            Assert.IsNotNull(location);
            Assert.IsNotNull(location.Classrooms);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LocationNameAndStreetAndHouseNumberAreEmptyThrowsError()
        {
            Location location = new Location(string.Empty, string.Empty,String.Empty, city1);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CityIsNullThrowsError()
        {
            testLocation = new Location();
            testLocation.Name = "Somewhere in Antwerpen";
            testLocation.City = null;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LocationNameIsEmptyThrowsError()
        {
            testLocation = new Location();
            testLocation.Name = String.Empty;

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testLocationIsNullThrowsError()
        {
            testLocation = new Location();
            testLocation.Name = null;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LocationIs101LongThrowsError()
        {
            testLocation = new Location();
            testLocation.Name =
                "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij1";
            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HouseNumberIs6LongThrowsError()
        {
            testLocation = new Location();
            testLocation.HouseNumber = "123456";
        }
        [TestMethod]
        public void HouseNumnerIs5LongCreatesIt()
        {
            testLocation = new Location();
            testLocation.HouseNumber = "12345";
            Assert.AreEqual("12345", testLocation.HouseNumber);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LocationNameIsOkButHouseNumberIsWhiteSpaceThrowsError()
        {
            testLocation = new Location();
            testLocation = new Location("Schoonmeersen", "TestStreet", "  ", city1);

        }
        [TestMethod]
        public void LocationnameIs100LongCreatesIt()
        {
            testLocation = new Location("abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij","Streettest","123",city1);
            Assert.AreEqual("abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij", testLocation.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LocationIsOkButCityIsNull()
        {
            testLocation = new Location("A Random Name","A Random Street", "1234",null);
        }
        [TestMethod]
        public void AddsClassRoomToTheLocation()
        {
            testLocation = new Location();
            testLocation.AddClassroom(classroom1);
            var addedClassRoom = testLocation.FindClassRoomByName("be54");
            Assert.AreEqual(classroom1.Name, addedClassRoom.Name);
              
        }
        public void RemovesClassRoomFromTheLocation()
        {
            testLocation = new Location();
            testLocation.AddClassroom(classroom1);
            testLocation.RemoveClassRoom(classroom1);
            Assert.AreEqual(0,testLocation.Classrooms.Count);
        }
        public void FindsClassRoomInTheLocation()
        {
            testLocation = new Location();
            testLocation.AddClassroom(classroom1);
            var foundClassRoom = testLocation.FindClassRoomByName("be54");
            Assert.AreEqual(classroom1, foundClassRoom);
        }
    }
}
