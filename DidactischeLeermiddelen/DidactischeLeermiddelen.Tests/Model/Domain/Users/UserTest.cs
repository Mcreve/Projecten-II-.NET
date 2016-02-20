using System;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Users
{
    [TestClass]
    public class UserTest
    {
        #region Arrange
        private User student;
        private User lector;
        #endregion


        [TestInitialize]
        public void UserTestInitialize()
        {
            student = UserFactory.CreateUser(UserType.Student);
            lector  = UserFactory.CreateUser(UserType.Lector);
        }

        [TestMethod]
        public void UserSetsNamesWhenTheyAreCorrect()
        {
            #region Arrange

            const string firstName = "Benjamin";
            const string lastName = "Vertonghen";
            #endregion

            #region Act
            student.FirstName = firstName;
            student.LastName = lastName;
            #endregion

            #region Assert
            Assert.AreEqual(firstName,student.FirstName);
            Assert.AreEqual(lastName,student.LastName);     
            #endregion

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenFirstNameHasNumericalCharacters()
        {
            #region Arrange
            const string name = "TestName123";
            #endregion

            #region Act
            student.FirstName = name;
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenLastNameHasNumericalCharacters()
        {
            #region Arrange
            const string name = "TestName123";
            #endregion

            #region Act
            student.LastName = name;
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenLastNameHas101Characters()
        {
            #region Arrange
            const string name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            #endregion

            #region Act
            student.LastName = name;
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenFirstNameHas101Characters()
        {
            #region Arrange
            const string name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            #endregion

            #region Act
            student.FirstName = name;
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenEmailIsNotFromHoGent()
        {
            #region Arrange
            const string email = "benjamin.vertonghen@victory-be.be";
            #endregion

            #region Act
            student.EmailAddress = email;
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenEmailDoesNotEndWithHogent()
        {
            #region Arrange
            const string email = "benjamin.hogent@hogent.be";
            #endregion

            #region Act
            student.EmailAddress = email;
            #endregion
        }
        [TestMethod]
        public void UserSetsEmailAddressForStudent()
        {
            #region Arrange
            const string email = "student.hogent.be";
            #endregion

            #region Act
            UserType userType = UserFactory.DetermineUserTypeByEmailAddress(email);
            User user = UserFactory.CreateUser(userType);
            user.EmailAddress = email;
            #endregion

            #region Assert
            Assert.AreEqual(email, user.EmailAddress);
            #endregion
        }
        [TestMethod]
        public void UserSetsEmailAddressForLector()
        {
            #region Arrange
            User tesUser = new Lector();
            const string email = "benjamin@hogent.be";
            #endregion

            #region Act
            tesUser.EmailAddress = email;
            #endregion

            #region Assert
            Assert.AreEqual(email, tesUser.EmailAddress);
            #endregion
        }
    }
}
