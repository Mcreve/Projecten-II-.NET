using System;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities
{
    [TestClass]
    public class LearningUtilityDetailsTest
    {
        #region Arrange
        private LearningUtilityDetails initiaLearningUtilityDetails;
        private Location initialLocation;
        private FieldOfStudy initialFieldOfStudy;
        private TargetGroup initialTargetGroup;
        private Company initialCompany;

        #endregion

        [TestInitialize]
        public void LearningUtilityDetailsTestInitialize()
        {
            initiaLearningUtilityDetails = new LearningUtilityDetails();
            initialLocation = new Location("GELDE 1.001");
            initialFieldOfStudy = new FieldOfStudy("Geschiedenis");
            initialTargetGroup = new TargetGroup("1e leerjaar");
            initialCompany = new Company("Verbe");
        }

        #region ConstructorTests
        [TestMethod]
        public void LearningUtilityDetailsDefaultConstructorAnCreatesAnObjectAndList()
        {
            #region Act
            LearningUtilityDetails learningUtilityDetail = new LearningUtilityDetails();
            #endregion

            #region Assert
            Assert.IsInstanceOfType(learningUtilityDetail, typeof(LearningUtilityDetails));
            Assert.IsNotNull(learningUtilityDetail.LearningUtilities);
            Assert.IsTrue(learningUtilityDetail.Loanable);
            Assert.AreEqual(0,learningUtilityDetail.Price);
            #endregion
        }

        [TestMethod]
        public void LearningUtilityDetailsParameterConstructorCreatesAnObjectAndList()
        {
            #region Arrange

            const string name = "Wereldbol";
            const string description = "Een wereldbol voor de lessen Aardrijkskunde!";
            Location location = new Location("GLEDE 1.011");
            #endregion

            #region Act
            LearningUtilityDetails learningUtilityDetail = new LearningUtilityDetails(name, description, location);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(learningUtilityDetail, typeof(LearningUtilityDetails));
            Assert.IsNotNull(learningUtilityDetail.LearningUtilities);
            Assert.IsTrue(learningUtilityDetail.Loanable);
            Assert.AreEqual(name, learningUtilityDetail.Name);
            Assert.AreEqual(description, learningUtilityDetail.Description);
            Assert.AreEqual(location.Name, learningUtilityDetail.Location.Name);
            Assert.AreEqual(0, learningUtilityDetail.Price);
            #endregion

        }
        #endregion

        #region NameTests
        [TestMethod]
        public void LearningUtilityDetailsNameIs50CharactersLongSetsTheName()
        {
            #region Arrange
            const string name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            #endregion

            #region Act

            initiaLearningUtilityDetails.Name = name;
            #endregion

            #region Assert
            Assert.AreEqual(50,name.Length);
            Assert.AreEqual(name, initiaLearningUtilityDetails.Name);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsNameIs101CharactersLongThrowsError()
        {
            #region Arrange
            const string name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            #endregion

            #region Act

            initiaLearningUtilityDetails.Name = name;
            #endregion

            #region Assert
            Assert.AreEqual(101, name.Length);
            Assert.AreEqual(name, initiaLearningUtilityDetails.Name);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsNameHasAlphaNumericNameSetsIt()
        {
            #region Arrange
            const string name = "Wereldkaart 1";
            #endregion

            #region Act

            initiaLearningUtilityDetails.Name = name;
            #endregion

            #region Assert
            Assert.AreEqual(name, initiaLearningUtilityDetails.Name);
            #endregion

        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsNameHasIsNullThrowsError()
        {
            #region Arrange
            const string name = null;
            #endregion

            #region Act

            initiaLearningUtilityDetails.Name = name;
            #endregion

        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsNameHasIsEmptyThrowsError()
        {
            #region Arrange
            string name = String.Empty;
            #endregion

            #region Act

            initiaLearningUtilityDetails.Name = name;
            #endregion
        }
        #endregion
        #region DescriptionTests
        [TestMethod]
        public void LearningUtilityDetailsDescriptionIs101CharactersLongSetsTheDescription()
        {
            #region Arrange
            const string description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            #endregion

            #region Act

            initiaLearningUtilityDetails.Description = description;
            #endregion

            #region Assert
            Assert.AreEqual(101, description.Length);
            Assert.AreEqual(description, initiaLearningUtilityDetails.Description);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsDescriptionIs1001CharactersLongThrowsError()
        {
            #region Act

            const string description =
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            #endregion

            #region Act

            initiaLearningUtilityDetails.Description = description;
            #endregion

            #region Assert
            Assert.AreEqual(1001, description.Length);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsDescriptionHasAlphaNumericDescriptionSetsIt()
        {
            #region Arrange
            const string description = "Wereldkaart 1 die ...";
            #endregion

            #region Act

            initiaLearningUtilityDetails.Description = description;
            #endregion

            #region Assert
            Assert.AreEqual(description, initiaLearningUtilityDetails.Description);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsDescriptionHasIsNullThrowsError()
        {
            #region Act

            initiaLearningUtilityDetails.Description = null;
            #endregion

        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsDescriptionHasIsEmptyThrowsError()
        {
            #region Act

            initiaLearningUtilityDetails.Description = String.Empty;
            #endregion
        }
        #endregion
        #region LocationTests
        [TestMethod]
        public void LearningUtilityDetailsLocationSetsIt()
        {
            #region Act

            initiaLearningUtilityDetails.Location = initialLocation;

            #endregion

            #region Assert
            Assert.AreEqual(initialLocation.Name,initiaLearningUtilityDetails.Location.Name);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsLocationIsNullThrowsError()
        {
            #region Act

            initiaLearningUtilityDetails.Location = null;

            #endregion
        }
        #endregion
        #region PriceTests
        [TestMethod]
        public void LearningUtilityDetailsPriceIs50SetsIt()
        {
            #region Arrange

            var price = 50M;

            #endregion

            #region Act
            initiaLearningUtilityDetails.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual((decimal)price,initiaLearningUtilityDetails.Price);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsPriceIsInt50SetsIt()
        {
            #region Arrange

            int price = 50;

            #endregion

            #region Act
            initiaLearningUtilityDetails.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual((decimal)price, initiaLearningUtilityDetails.Price);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsPriceIsDecimalSetsIt()
        {
            #region Arrange

            decimal price = 0.87M;

            #endregion

            #region Act
            initiaLearningUtilityDetails.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual((decimal)price, initiaLearningUtilityDetails.Price);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsPriceIsNegativeThrowsError()
        {
            #region Arrange

            var price = Decimal.MinValue;

            #endregion

            #region Act
            initiaLearningUtilityDetails.Price = price;
            #endregion

        }
        [TestMethod]
        public void LearningUtilityDetailsPriceIsNullSetsIt()
        {
            #region Arrange

            decimal? price = null;

            #endregion

            #region Act
            initiaLearningUtilityDetails.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual((decimal)price, initiaLearningUtilityDetails.Price);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsPriceIsZeroSetsIt()
        {
            #region Arrange

            decimal? price = Decimal.Zero;

            #endregion

            #region Act
            initiaLearningUtilityDetails.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual((decimal)price, initiaLearningUtilityDetails.Price);
            #endregion
        }
        #endregion
        #region ArticleNumberTests
        [TestMethod]
        public void LearningUtilityDetailsArticleNumberIs50CharactersLongSetsTheArticleNumber()
        {
            #region Arrange
            const string articleNr = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            #endregion

            #region Act

            initiaLearningUtilityDetails.ArticleNumber = articleNr;
            #endregion

            #region Assert
            Assert.AreEqual(50, articleNr.Length);
            Assert.AreEqual(articleNr, initiaLearningUtilityDetails.ArticleNumber);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsArticleNumberIs101CharactersLongThrowsError()
        {
            #region Arrange
            const string articleNr = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            #endregion

            #region Act

            initiaLearningUtilityDetails.ArticleNumber = articleNr;
            #endregion

            #region Assert
            Assert.AreEqual(101, articleNr.Length);
            Assert.AreEqual(articleNr, initiaLearningUtilityDetails.ArticleNumber);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsArticleNumberHasAlphaNumericArticleNumberSetsIt()
        {
            #region Arrange
            const string articleNr = "FHZ.123";
            #endregion

            #region Act

            initiaLearningUtilityDetails.ArticleNumber = articleNr;
            #endregion

            #region Assert
            Assert.AreEqual(articleNr, initiaLearningUtilityDetails.ArticleNumber);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsArticleNumberHasIsNullSetsIt()
        {
            #region Arrange
            const string articleNr = null;
            #endregion

            #region Act

            initiaLearningUtilityDetails.ArticleNumber = articleNr;

            #endregion

            #region Assert
            Assert.AreEqual(articleNr, initiaLearningUtilityDetails.ArticleNumber);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsArticleNumberHasIsEmptySetsIt()
        {
            #region Arrange
            string articleNr = String.Empty;
            #endregion

            #region Act

            initiaLearningUtilityDetails.ArticleNumber = articleNr;

            #endregion

            #region Assert
            Assert.AreEqual(articleNr, initiaLearningUtilityDetails.ArticleNumber);
            #endregion
        }
        #endregion
        #region FieldOfStudyTests
        [TestMethod]
        public void LearningUtilityDetailsFieldOfStudySetsIt()
        {
            #region Act

            initiaLearningUtilityDetails.FieldOfStudy = initialFieldOfStudy;

            #endregion

            #region Assert
            Assert.AreEqual(initialFieldOfStudy.Name,initiaLearningUtilityDetails.FieldOfStudy.Name);
            #endregion
        }

        #endregion
        #region TargetGroupTests
        [TestMethod]
        public void LearningUtilityDetailsTargetGroupSetsIt()
        {
            #region Act

            initiaLearningUtilityDetails.TargetGroup = initialTargetGroup;

            #endregion

            #region Assert
            Assert.AreEqual(initialTargetGroup.Name,initiaLearningUtilityDetails.TargetGroup.Name);
            #endregion
        }
        #endregion
        #region CompanyTests
        [TestMethod]
        public void LearningUtilityDetailsCompanySetsIt()
        {
            #region Act

            initiaLearningUtilityDetails.Company = initialCompany;

            #endregion

            #region Assert
            Assert.AreEqual(initialCompany.Name, initiaLearningUtilityDetails.Company.Name);
            #endregion
        }
        #endregion
    }
}
