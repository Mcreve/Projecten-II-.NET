using System;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities
{
    [TestClass]
    public class FieldOfStudyTest
    {
        private FieldOfStudy fieldOfStudy;

        [TestMethod]
        public void FieldOfStudyDefaultConstructorCreatesAFieldOfStudy()
        {

            #region Act
            fieldOfStudy = new FieldOfStudy();
            #endregion

            #region Assert
            Assert.IsNotNull(fieldOfStudy);
            Assert.IsInstanceOfType(fieldOfStudy, typeof(FieldOfStudy)); 
            #endregion


        }
        [TestMethod]
        public void FieldOfStudyParameterConstructorCreatesAFieldOfStudy()
        {
            #region Arrange
            const string fieldOfStudyName = "Preparatie";
            #endregion

            #region Act
            fieldOfStudy = new FieldOfStudy(fieldOfStudyName);
            #endregion

            #region Assert
            Assert.AreEqual(fieldOfStudyName, fieldOfStudy.Name);
            #endregion
        }
        [TestMethod]
        public void FieldOfStudyNameIs50CharactersLongSetsTheName()
        {
            #region Arrange
            const string fieldOfStudyName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int fieldOfStudyNameLength = fieldOfStudyName.Length;
            #endregion

            #region Act
            fieldOfStudy = new FieldOfStudy(fieldOfStudyName);
            #endregion

            #region Assert
            Assert.AreEqual(fieldOfStudyName, fieldOfStudy.Name);
            Assert.AreEqual(50, fieldOfStudyNameLength);
            #endregion
        }
        [TestMethod]
        public void FieldOfStudyNameIs100CharactersLongSetsTheName()
        {
            #region Arrange
            const string fieldOfStudyName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int fieldOfStudyNameLength = fieldOfStudyName.Length;
            #endregion

            #region Act
            fieldOfStudy = new FieldOfStudy(fieldOfStudyName);
            #endregion

            #region Assert
            Assert.AreEqual(fieldOfStudyName, fieldOfStudy.Name);
            Assert.AreEqual(100, fieldOfStudyNameLength);
            #endregion
        }
        
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FieldOfStudyNameIs101CharactersLongThrowsError()
        {
            #region Arrange
            const string fieldOfStudyName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            int fieldOfStudyNameLength = fieldOfStudyName.Length;
            #endregion

            #region Act
            fieldOfStudy = new FieldOfStudy(fieldOfStudyName);
            #endregion

            #region Assert
            Assert.AreEqual(101, fieldOfStudyNameLength);
            #endregion
        }
        [TestMethod]
        public void FieldOfStudyNameHasAlphaNumericNameCreatesIt()
        {
            #region Arrange
            const string fieldOfStudyName = "GLEDE 1.011";
            #endregion

            #region Act
            fieldOfStudy = new FieldOfStudy(fieldOfStudyName);
            #endregion

            #region Assert
            Assert.AreEqual(fieldOfStudyName, fieldOfStudy.Name);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void fieldOfStudyNameHasIsNullThrowsError()
        {
            #region Arrange
            string fieldOfStudyName = null;
            #endregion

            #region Act
            fieldOfStudy = new FieldOfStudy(fieldOfStudyName);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FieldOfStudyNameHasIsEmptyThrowsError()
        {
            #region Arrange
            string fieldOfStudyName = string.Empty;
            #endregion

            #region Act
            fieldOfStudy = new FieldOfStudy(fieldOfStudyName);
            #endregion
        }
    }
}
