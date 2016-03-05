using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
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
        private LearningUtilityReservation reservation;

        #endregion

        [TestInitialize]
        public void LearningUtilityDetailsTestInitialize()
        {
            initiaLearningUtilityDetails = new LearningUtilityDetails();
            initialLocation = new Location("GELDE 1.001");
            initialFieldOfStudy = new FieldOfStudy("Geschiedenis");
            initialTargetGroup = new TargetGroup("1e leerjaar");
            initialCompany = new Company("Verbe");
            reservation = new LearningUtilityReservation {Week = 1, Amount = 5};
            initiaLearningUtilityDetails.LearningUtilityReservations.Add(reservation);
            initiaLearningUtilityDetails.AmountInCatalog = 10;
            initiaLearningUtilityDetails.AmountUnavailable = 5;
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
            Assert.IsTrue(learningUtilityDetail.Loanable);
            Assert.AreEqual(name, learningUtilityDetail.Name);
            Assert.AreEqual(description, learningUtilityDetail.Description);
            Assert.AreEqual(location.Name, learningUtilityDetail.Location.Name);
            Assert.AreEqual(0, learningUtilityDetail.Price);
            #endregion

        }
        #endregion

        #region Properties

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
            Assert.AreEqual(price, initiaLearningUtilityDetails.Price);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
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
        public void LearningUtilityDetailsPriceIsZeroSetsIt()
        {
            #region Arrange

            decimal price = Decimal.Zero;

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

            initiaLearningUtilityDetails.FieldsOfStudy.Add(initialFieldOfStudy);

            #endregion

            #region Assert
            Assert.AreEqual(initialFieldOfStudy,initiaLearningUtilityDetails.FieldsOfStudy.Single(f => f.Equals(initialFieldOfStudy)));
            #endregion
        }

        #endregion

        #region TargetGroupTests
        [TestMethod]
        public void LearningUtilityDetailsTargetGroupSetsIt()
        {
            #region Act

            initiaLearningUtilityDetails.TargetGroups.Add(initialTargetGroup);

            #endregion

            #region Assert
            Assert.AreEqual(initialTargetGroup,initiaLearningUtilityDetails.TargetGroups.Single(t => t.Equals(initialTargetGroup)));
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

        #region Loanable
        [TestMethod]
        public void LearningUtilityDetailsLoanableSetsIt()
        {
            #region Act

            initiaLearningUtilityDetails.Loanable = true;

            #endregion

            #region Assert
            Assert.AreEqual(true, initiaLearningUtilityDetails.Loanable);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsUnloanableSetsIt()
        {
            #region Act

            initiaLearningUtilityDetails.Loanable = false;

            #endregion

            #region Assert
            Assert.AreEqual(false, initiaLearningUtilityDetails.Loanable);
            #endregion
        }
        #endregion

        #region PictureTests
        [TestMethod]
        public void LearningUtilityDetailsPictureUrlSetsIt()
        {
            #region Arrange
            const string pictureUrl = "/items/pictures/wereldbol.jpg";
            #endregion

            #region Act
            initiaLearningUtilityDetails.Picture = pictureUrl;
            #endregion

            #region Assert
            Assert.AreEqual(pictureUrl, initiaLearningUtilityDetails.Picture);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsPictureUrlIsEmptySetsIt()
        {
            #region Arrange
            string pictureUrl = string.Empty;
            #endregion

            #region Act
            initiaLearningUtilityDetails.Picture = pictureUrl;
            #endregion

            #region Assert
            Assert.AreEqual(pictureUrl, initiaLearningUtilityDetails.Picture);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDetailsPictureURLIs251CharactersLongThrowsError()
        {
            #region Arrange

            const string url =
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + //100
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + //100
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab"; //51
            #endregion

            #region Act

            initiaLearningUtilityDetails.Picture = url;
            #endregion

            #region Assert
            Assert.AreEqual(251, url.Length);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDetailsPictureURLIs250CharactersLongSetsIt()
        {
            #region Arrange

            const string url =
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + //100
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + //100
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"; //50
            #endregion

            #region Act

            initiaLearningUtilityDetails.Picture = url;
            #endregion

            #region Assert
            Assert.AreEqual(250, url.Length);
            Assert.AreEqual(url, initiaLearningUtilityDetails.Picture);
            #endregion
        }
        #endregion

        #endregion

        #region Methods

        #region AmountAvailableForWeek
        [TestMethod]
        public void AmountAvailableForWeekWithReservationsReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtilityDetails.AmountAvailableForWeek(1, 0);

            //Assert
            Assert.AreEqual(0, amount);
        }

        [TestMethod]
        public void AmountAvailableForWeekWithoutReservationsReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtilityDetails.AmountAvailableForWeek(2, 0);

            //Assert
            Assert.AreEqual(5, amount);
        }

        [TestMethod]
        public void AmountAvailableForWeekWithCurrentWeekHigherThanReservedWeekReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtilityDetails.AmountAvailableForWeek(5, 2);

            //Assert
            Assert.AreEqual(0, amount);
        }
        #endregion

        #region AmountReservedForWeek
        [TestMethod]
        public void AmountReservedForWeekWithReservationsReturnsCorrectValue()
        {
            //Arrange
            initiaLearningUtilityDetails.LearningUtilityReservations.First().User = new Student();

            //Act
            int amount = initiaLearningUtilityDetails.AmountReservedForWeek(1);

            //Assert
            Assert.AreEqual(5, amount);
        }

        [TestMethod]
        public void AmountReservedForWeekWithoutReservationsReturnsZero()
        {
            //Arrange
            initiaLearningUtilityDetails.LearningUtilityReservations.First().User = new Student();

            //Act
            int amount = initiaLearningUtilityDetails.AmountReservedForWeek(2);

            //Assert
            Assert.AreEqual(0, amount);
        }

        [TestMethod]
        public void AmountReservedForWeekWithReservationsByLectorReturnsZero()
        {
            //Arrange
            initiaLearningUtilityDetails.LearningUtilityReservations.First().User = new Lector();

            //Act
            int amount = initiaLearningUtilityDetails.AmountReservedForWeek(1);

            //Assert
            Assert.AreEqual(0, amount);
        }
        #endregion

        #region AmountBlockedForWeek
        [TestMethod]
        public void AmountBlockedForWeekWithReservationsReturnsCorrectValue()
        {
            //Arrange
            initiaLearningUtilityDetails.LearningUtilityReservations.First().User = new Lector();

            //Act
            int amount = initiaLearningUtilityDetails.AmountBlockedForWeek(1);

            //Assert
            Assert.AreEqual(5, amount);
        }

        [TestMethod]
        public void AmountBlockedForWeekWithoutReservationsReturnsZero()
        {
            //Arrange
            initiaLearningUtilityDetails.LearningUtilityReservations.First().User = new Lector();

            //Act
            int amount = initiaLearningUtilityDetails.AmountBlockedForWeek(2);

            //Assert
            Assert.AreEqual(0, amount);
        }

        [TestMethod]
        public void AmountBlockedForWeekWithReservationsByStudentReturnsZero()
        {
            //Arrange
            initiaLearningUtilityDetails.LearningUtilityReservations.First().User = new Student();

            //Act
            int amount = initiaLearningUtilityDetails.AmountBlockedForWeek(1);

            //Assert
            Assert.AreEqual(0, amount);
        }
        #endregion

        #region AmountUnavailableForWeek
        [TestMethod]
        public void AmountUnavailableForWeekWithReservationsByStudentReturnsCorrectValue()
        {
            //Arrange
            initiaLearningUtilityDetails.LearningUtilityReservations.First().User = new Student();

            //Act
            int amount = initiaLearningUtilityDetails.AmountUnavailableForWeek(1, 0);

            //Assert
            Assert.AreEqual(10, amount);
        }

        [TestMethod]
        public void AmountUnavailableForWeekWithReservationsByLectorReturnsCorrectValue()
        {
            //Arrange
            initiaLearningUtilityDetails.LearningUtilityReservations.First().User = new Lector();

            //Act
            int amount = initiaLearningUtilityDetails.AmountUnavailableForWeek(1, 0);

            //Assert
            Assert.AreEqual(10, amount);
        }

        [TestMethod]
        public void AmountUnavailableForWeekWithoutReservationsReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtilityDetails.AmountUnavailableForWeek(5, 0);

            //Assert
            Assert.AreEqual(5, amount);
        }

        [TestMethod]
        public void AmountUnavailableForWeekWithCurrentWeekHigherThanReservedWeekReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtilityDetails.AmountUnavailableForWeek(5, 2);

            //Assert
            Assert.AreEqual(10, amount);
        }
        #endregion
        #endregion
    }
}
