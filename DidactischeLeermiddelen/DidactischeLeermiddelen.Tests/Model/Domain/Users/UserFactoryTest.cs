using System;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Users
{
    [TestClass]
    public class UserFactoryTest
    {
        [TestMethod]
        public void UserFactoryWithParametersCreatesStudent()
        {
            #region Arrange
            string firstName = "Benjamin";
            string lastName = "Vertonghen";
            string emailAddress = "Benjamin.vertonghen@student.hogent.be";
            #endregion

            #region Act
            student = UserFactory.CreateUserWithParameters(firstName, lastName, emailAddress);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(student, typeof (Student));
            Assert.AreEqual(firstName,student.FirstName);
            Assert.AreEqual(lastName,student.LastName);
            Assert.AreEqual(emailAddress,student.EmailAddress);
            #endregion

        }
        [TestMethod]
        public void UserFactoryWithUserTypeCreatesStudent()
        {
            #region Act

            student = UserFactory.CreateUserWithUserType(UserType.Student);

            #endregion

            #region Assert

            Assert.IsInstanceOfType(student, typeof(Student));


            #endregion
        }

        [TestMethod]
        public void UserFactoryWithParametersCreatesLector()
        {
            #region Arrange
            string firstName = "Benjamin";
            string lastName = "Vertonghen";
            string emailAddress = "Benjamin.vertonghen@hogent.be";
            #endregion

            #region Act
            lector = UserFactory.CreateUserWithParameters(firstName, lastName, emailAddress);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(lector, typeof(Lector));
            Assert.AreEqual(firstName, lector.FirstName);
            Assert.AreEqual(lastName, lector.LastName);
            Assert.AreEqual(emailAddress, lector.EmailAddress);
            #endregion
        }
        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void UserFactoryWithParametersButFirstNameIsEmptyThrowsError()
        {
            #region Arrange
            string firstName = "";
            string lastName = "Vertonghen";
            string emailAddress = "Benjamin.vertonghen@hogent.be";
            #endregion

            #region Act
            lector = UserFactory.CreateUserWithParameters(firstName, lastName, emailAddress);
            #endregion
        }
        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void UserFactoryWithParametersButLastNameIsEmptyThrowsError()
        {
            #region Arrange
            string firstName = "Benjamin";
            string lastName = string.Empty;
            string emailAddress = "Benjamin.vertonghen@hogent.be";
            #endregion

            #region Act
            lector = UserFactory.CreateUserWithParameters(firstName, lastName, emailAddress);
            #endregion
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void UserFactoryWithParametersButEmailAddressIsEmptyThrowsError()
        {
            #region Arrange
            string firstName = "Benjamin";
            string lastName = "Vertonghen";
            string emailAddress = string.Empty;
            #endregion

            #region Act
            lector = UserFactory.CreateUserWithParameters(firstName, lastName, emailAddress);
            #endregion
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void UserFactoryWithParametersButEmailAddressIsNullThrowsError()
        {
            #region Arrange
            string firstName = "Benjamin";
            string lastName = "Vertonghen";
            string emailAddress = null;
            #endregion

            #region Act
            lector = UserFactory.CreateUserWithParameters(firstName, lastName, emailAddress);
            #endregion
        }
        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void UserFactoryWithParametersButFirstNameIsNullThrowsError()
        {
            #region Arrange
            string firstName = null;
            string lastName = "Vertonghen";
            string emailAddress = "Benjamin.vertonghen@hogent.be";
            #endregion

            #region Act
            lector = UserFactory.CreateUserWithParameters(firstName, lastName, emailAddress);
            #endregion
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void UserFactoryWithParametersButEmailIsNotFromHoGent()
        {
            #region Arrange
            string firstName = "Benjamin";
            string lastName = "Vertonghen";
            string emailAddress = "Benjamin.vertonghen@victory.be";
            #endregion

            #region Act
            lector = UserFactory.CreateUserWithParameters(firstName, lastName, emailAddress);
            #endregion
        }
        [TestMethod]
        public void UserFactoryWithUserTypeCreatesLector()
        {
            #region Act

            lector = UserFactory.CreateUserWithUserType(UserType.Lector);

            #endregion

            #region Assert

            Assert.IsInstanceOfType(lector, typeof(Lector));

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (IndexOutOfRangeException))]
        public void UserFactoryThrowsErrorWhenTypeIsOutOfRangeOfUserType()
        {
            #region Arrange

            //Will always be out off range(0 based index) if the Enum is sequentially structured!
            var amountOfItemsInEnum = Enum.GetValues(typeof (UserType)).Length;

            #endregion

            #region Act

            var notAUserType = UserFactory.CreateUserWithUserType((UserType)amountOfItemsInEnum);

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (IndexOutOfRangeException))]
        public void UserFactoryThrowsErrorWhenUserTypeIsInvalid()
        {
            #region Act

            invalid = UserFactory.CreateUserWithUserType(UserType.Invalid);

            #endregion
        }

        [TestMethod]
        public void UserFactoryDeterminesCorrectRoleForStudent()
        {
            #region Arrange

            const string emailAdress = "ben.vertonghen@student.hogent.be";
            expectedUserType = UserType.Student;

            #endregion

            #region Act

            actualUserType = UserFactory.DetermineUserTypeByEmailAddress(emailAdress);

            #endregion

            #region Assert

            Assert.AreEqual(expectedUserType, actualUserType);

            #endregion
        }

        [TestMethod]
        public void UserFactoryDeterminesCorrectRoleForLector()
        {
            #region Arrange

            const string emailAdress = "ben.vertonghen@hogent.be";
            expectedUserType = UserType.Lector;

            #endregion

            #region Act

            actualUserType = UserFactory.DetermineUserTypeByEmailAddress(emailAdress);

            #endregion

            #region Assert

            Assert.AreEqual(expectedUserType, actualUserType);

            #endregion
        }

        [TestMethod]
        public void UserFactoryDeterminesCorrectRoleForInvalid()
        {
            #region Arrange

            const string emailAdress = "ben.vertonghen@victory.be";
            expectedUserType = UserType.Invalid;

            #endregion

            #region Act

            actualUserType = UserFactory.DetermineUserTypeByEmailAddress(emailAdress);

            #endregion

            #region Assert

            Assert.AreEqual(expectedUserType, actualUserType);

            #endregion
        }

        [TestMethod]
        public void UserFactoryDeterminesInvalidForEmailThatContainsHoGentButNotAtTheEnd()
        {
            #region Arrange

            const string emailAdress = "ben.hogent@victory.be";
            expectedUserType = UserType.Invalid;

            #endregion

            #region Act

            actualUserType = UserFactory.DetermineUserTypeByEmailAddress(emailAdress);

            #endregion

            #region Assert

            Assert.AreEqual(expectedUserType, actualUserType);

            #endregion
        }

        #region Arrange

        private User student;
        private User lector;
        private User invalid;
        private UserType actualUserType;
        private UserType expectedUserType;

        #endregion
    }
}