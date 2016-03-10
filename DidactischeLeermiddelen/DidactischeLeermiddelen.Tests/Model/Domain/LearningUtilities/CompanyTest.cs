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

    }
}
