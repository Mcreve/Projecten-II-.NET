using System;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities
{
    [TestClass]
    public class CompanyTest
    {
        private Company company;

        [TestMethod]
        public void CompanyDefaultConstructorCreatesACompany()
        {

            #region Act
            company = new Company();
            #endregion

            #region Assert
            Assert.IsNotNull(company);
            Assert.IsInstanceOfType(company, typeof(Company)); 
            #endregion


        }
        [TestMethod]
        public void CompanyParameterConstructorCreatesACompany()
        {
            #region Arrange
            const string companyName = "Preparatie";
            #endregion

            #region Act
            company = new Company(companyName);
            #endregion

            #region Assert
            Assert.AreEqual(companyName, company.Name);
            #endregion
        }
        [TestMethod]
        public void companyNameIs50CharactersLongSetsTheName()
        {
            #region Arrange
            const string companyName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int companyNameLength = companyName.Length;
            #endregion

            #region Act
            company = new Company(companyName);
            #endregion

            #region Assert
            Assert.AreEqual(companyName, company.Name);
            Assert.AreEqual(50, companyNameLength);
            #endregion
        }
        [TestMethod]
        public void companyNameIs100CharactersLongSetsTheName()
        {
            #region Arrange
            const string companyName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int companyNameLength = companyName.Length;
            #endregion

            #region Act
            company = new Company(companyName);
            #endregion

            #region Assert
            Assert.AreEqual(companyName, company.Name);
            Assert.AreEqual(100, companyNameLength);
            #endregion
        }
        
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void companyNameIs101CharactersLongThrowsError()
        {
            #region Arrange
            const string companyName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            int companyNameLength = companyName.Length;
            #endregion

            #region Act
            company = new Company(companyName);
            #endregion

            #region Assert
            Assert.AreEqual(101, companyNameLength);
            #endregion
        }
        [TestMethod]
        public void companyNameHasAlphaNumericNameCreatesIt()
        {
            #region Arrange
            const string companyName = "GLEDE 1.011";
            #endregion

            #region Act
            company = new Company(companyName);
            #endregion

            #region Assert
            Assert.AreEqual(companyName, company.Name);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void companyNameHasIsNullThrowsError()
        {
            #region Arrange
            string companyName = null;
            #endregion

            #region Act
            company = new Company(companyName);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void companyNameHasIsEmptyThrowsError()
        {
            #region Arrange
            string companyName = string.Empty;
            #endregion

            #region Act
            company = new Company(companyName);
            #endregion
        }
    }
}
