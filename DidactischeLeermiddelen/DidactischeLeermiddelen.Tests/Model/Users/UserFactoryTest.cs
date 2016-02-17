using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Tests.Model.Users
{
    [TestClass]
    public class UserFactoryTest
    {
        #region Arrange

        private IUser student;
        private IUser lector;

        
        #endregion

        [TestMethod]
        public void UserFactoryCreatesStudent()
        {

            #region Act
            student = UserFactory.CreateUser(UserType.Student);

            #endregion

            #region Assert
            Assert.IsInstanceOfType(student,typeof (Student));
            #endregion

        }

        [TestMethod]
        public void UserFactoryCreatesLector()
        {
            #region Act
            lector = UserFactory.CreateUser(UserType.Lector);

            #endregion

            #region Assert
            Assert.IsInstanceOfType(lector, typeof(Lector));
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void UserFactoryThrowsErrorWhenTypeIsOutOfRangeOfUserType()
        {
            #region Arrange
            //Will always be out off range(0 based index) if the Enum is sequentially structured!
            var amountOfItemsInEnum = Enum.GetValues(typeof (UserType)).Length;
            #endregion

            #region Act
            var notAUserType = UserFactory.CreateUser((UserType)amountOfItemsInEnum);
            #endregion
        }
    }
}
