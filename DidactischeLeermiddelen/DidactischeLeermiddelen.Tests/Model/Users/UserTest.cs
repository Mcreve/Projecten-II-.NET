using System;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Users
{
    [TestClass]
    public class UserTest
    {
        private IUser student;
        private IUser lector;

        [TestInitialize]
        public void UserTestInitialize()
        {
            student = UserFactory.CreateUser(UserType.Student);
            lector  = UserFactory.CreateUser(UserType.Lector);
        }


        [TestMethod]
        public void OnlyAlphanumericCharactersForFirstAndLastName()
        {
            student.FirstName = "123123123123";
            Console.WriteLine(student.FirstName);

        }
    }
}
