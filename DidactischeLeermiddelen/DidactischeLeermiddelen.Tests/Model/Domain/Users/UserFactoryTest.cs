using System;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Users
{
    [TestClass]
    public class UserFactoryTest
    {
        [TestMethod]
        public void UserFactoryCreatesStudent()
        {
            #region Act

            student = UserFactory.CreateUser(UserType.Student);

            #endregion

            #region Assert

            Assert.IsInstanceOfType(student, typeof (Student));

            #endregion
        }

        [TestMethod]
        public void UserFactoryCreatesLector()
        {
            #region Act

            lector = UserFactory.CreateUser(UserType.Lector);

            #endregion

            #region Assert

            Assert.IsInstanceOfType(lector, typeof (Lector));

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

            var notAUserType = UserFactory.CreateUser((UserType) amountOfItemsInEnum);

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (IndexOutOfRangeException))]
        public void UserFactoryThrowsErrorWhenUserTypeIsInvalid()
        {
            #region Act

            invalid = UserFactory.CreateUser(UserType.Invalid);

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