using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model
{
    [TestClass]
    public class ApplicationUserTest
    {
        private ApplicationUser initialUser;
        private string role;

       [TestInitialize]
        public void ApplicationUserTestInitialize()
        {
            const string userEmail = "Benjamin.vertonghen@student.hogent.be";
            initialUser = new ApplicationUser
            {
                UserName = userEmail,
                Email = userEmail
            };
        }

        [TestMethod]
        public void AssignStudentRoleToUser()
        {
            role = initialUser.GetRole(initialUser.Email);
            Assert.AreEqual("student", role);
        }
        [TestMethod]
        public void AssignStudentRoleToUserCamelCase()
        {
            initialUser.Email = "Benjamin.vertonghen@Student.Hogent.Be";
            role = initialUser.GetRole(initialUser.Email);
            Assert.AreEqual("student", role);
        }
        [TestMethod]
        public void AssignLectorRoleToUser()
        {
            initialUser.Email = "ben.vertonghen@hogent.be";
            role = initialUser.GetRole(initialUser.Email);
            Assert.AreEqual("lector", role);
        }
        [TestMethod]
        public void AssignLectorRoleToUserCamelCase()
        {
            initialUser.Email = "ben.vertonghen@HoGent.Be";
            role = initialUser.GetRole(initialUser.Email);
            Assert.AreEqual("lector", role);
        }
        [TestMethod]
        public void ReturnNullIfUserHasNoHoGentEmailAdress()
        {
            initialUser.Email = "ben.vertonghen@gmail.com";
            role = initialUser.GetRole(initialUser.Email);
            Assert.IsNull(role);

        }
        [TestMethod]
        public void ReturnNullIfUserHasNoHoGentEmailAdressCamelCase()
        {
            initialUser.Email = "ben.vertonghen@GmaiL.Be";
            role = initialUser.GetRole(initialUser.Email);
            Assert.IsNull(role);

        }
        [TestMethod]
        public void ReturnNullIfEmailContainsHoGentButNotAtTheEnd()
        {
            initialUser.Email = "ben.vertonghen.hogent.be@gmail.com";
            role = initialUser.GetRole(initialUser.Email);
            Assert.IsNull(role);

        }
    }
}
