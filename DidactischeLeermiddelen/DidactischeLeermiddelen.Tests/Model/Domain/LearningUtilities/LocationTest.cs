using System;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities
{
    [TestClass]
    public class LocationTest
    {
        private Location location;

        [TestMethod]
        public void LocationDefaultConstructorCreatesALocation()
        {

            #region Act
            location = new Location();
            #endregion

            #region Assert
            Assert.IsNotNull(location);
            Assert.IsInstanceOfType(location, typeof(Location)); 
            #endregion


        }
        [TestMethod]
        public void LocationParameterConstructorCreatesALocation()
        {
            #region Arrange
            const string locationName = "Preparatie";
            #endregion

            #region Act
            location = new Location(locationName);
            #endregion

            #region Assert
            Assert.AreEqual(locationName,location.Name);
            #endregion
        }
        [TestMethod]
        public void LocationNameIs50CharactersLongSetsTheName()
        {
            #region Arrange
            const string locationName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int locationNameLength = locationName.Length;
            #endregion

            #region Act
            location = new Location(locationName);
            #endregion

            #region Assert
            Assert.AreEqual(locationName, location.Name);
            Assert.AreEqual(50, locationNameLength);
            #endregion
        }
        [TestMethod]
        public void LocationNameIs100CharactersLongSetsTheName()
        {
            #region Arrange
            const string locationName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int locationNameLength = locationName.Length;
            #endregion

            #region Act
            location = new Location(locationName);
            #endregion

            #region Assert
            Assert.AreEqual(locationName, location.Name);
            Assert.AreEqual(100, locationNameLength);
            #endregion
        }
        
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LocationNameIs101CharactersLongThrowsError()
        {
            #region Arrange
            const string locationName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            int locationNameLength = locationName.Length;
            #endregion

            #region Act
            location = new Location(locationName);
            #endregion

            #region Assert
            Assert.AreEqual(101, locationNameLength);
            #endregion
        }
        [TestMethod]
        public void LocationNameHasAlphaNumericNameSetsIt()
        {
            #region Arrange
            const string locationName = "GLEDE 1.011";
            #endregion

            #region Act
            location = new Location(locationName);
            #endregion

            #region Assert
            Assert.AreEqual(locationName, location.Name);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LocationNameHasIsNullThrowsError()
        {
            #region Arrange
            string locationName = null;
            #endregion

            #region Act
            location = new Location(locationName);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LocationNameHasIsEmptyThrowsError()
        {
            #region Arrange
            string locationName = string.Empty;
            #endregion

            #region Act
            location = new Location(locationName);
            #endregion
        }
    }
}
