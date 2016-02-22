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
        #endregion

        [TestInitialize]
        public void LearningUtilityDetailsTestInitialize()
        {
            initiaLearningUtilityDetails = new LearningUtilityDetails();
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
        public void LearningUtilityDetailsDescriptionIs500CharactersLongSetsTheDescription()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsDescriptionIs1001CharactersLongThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsDescriptionHasAlphaNumericDescriptionSetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsDescriptionHasIsNullThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsDescriptionHasIsEmptyThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        #endregion
        #region LocationTests
        [TestMethod]
        public void LearningUtilityDetailsLocationSetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsLocationIsNullThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsLocationIsEmptyThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        #endregion
        #region PriceTests
        [TestMethod]
        public void LearningUtilityDetailsPriceIs50SetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsPriceIsNegativeThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsPriceIsNullSetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsPriceIsZeroSetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        #endregion
        #region ArticleNumberTests
        [TestMethod]
        public void LearningUtilityDetailsArticleNumberIs50CharactersLongSetsTheArticleNumber()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsArticleNumberIs101CharactersLongThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsArticleNumberHasAlphaNumericArticleNumberSetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsArticleNumberHasIsNullThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsArticleNumberHasIsEmptyThrowsError()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        #endregion
        #region FieldOfStudyTests
        [TestMethod]
        public void LearningUtilityDetailsFieldOfStudySetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }

        #endregion
        #region TargetGroupTests
        [TestMethod]
        public void LearningUtilityDetailsTargetGroupSetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        #endregion
        #region CompanyTests
        [TestMethod]
        public void LearningUtilityDetailsCompanySetsIt()
        {
            #region Act

            #endregion

            #region Assert

            #endregion
        }
        #endregion
    }
}
