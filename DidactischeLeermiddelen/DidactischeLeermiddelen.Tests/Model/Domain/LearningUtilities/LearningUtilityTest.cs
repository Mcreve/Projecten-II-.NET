using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities
{
    [TestClass]
    public class LearningUtilityTest
    {
        #region Arrange
        private LearningUtility initiaLearningUtility;
        private Location initialLocation;
        private FieldOfStudy initialFieldOfStudy;
        private TargetGroup initialTargetGroup;
        private Company initialCompany;
        private Reservation reservation;
        private DateTime date1;
        private DateTime date2;
        private DateTime date3;

        #endregion

        [TestInitialize]
        public void LearningUtilityTestInitialize()
        {
            initiaLearningUtility = new LearningUtility();
            initialLocation = new Location("GELDE 1.001");
            initialFieldOfStudy = new FieldOfStudy("Geschiedenis");
            initialTargetGroup = new TargetGroup("1e leerjaar");
            initialCompany = new Company("Verbe");
            reservation = new Reservation {Week = 11, Amount = 5};
            initiaLearningUtility.Reservations.Add(reservation);
            initiaLearningUtility.AmountInCatalog = 10;
            initiaLearningUtility.AmountUnavailable = 5;
            date1 = new DateTime(2016, 3, 16, 8, 30, 52);
            date2 = new DateTime(2016, 4, 8, 8, 30, 52);
            date3 = new DateTime(2016, 3, 8, 8, 30, 52);
        }

        #region ConstructorTests
        [TestMethod]
        public void LearningUtilityDefaultConstructorAnCreatesAnObjectAndList()
        {
            #region Act
            LearningUtility learningUtilityDetail = new LearningUtility();
            #endregion

            #region Assert
            Assert.IsInstanceOfType(learningUtilityDetail, typeof(LearningUtility));
            Assert.IsTrue(learningUtilityDetail.Loanable);
            Assert.AreEqual(0,learningUtilityDetail.Price);
            #endregion
        }

        [TestMethod]
        public void LearningUtilityParameterConstructorCreatesAnObjectAndList()
        {
            #region Arrange

            const string name = "Wereldbol";
            const string description = "Een wereldbol voor de lessen Aardrijkskunde!";
            Location location = new Location("GLEDE 1.011");
            #endregion

            #region Act
            LearningUtility learningUtilityDetail = new LearningUtility(name, description, location);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(learningUtilityDetail, typeof(LearningUtility));
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
        public void LearningUtilityNameIs50CharactersLongSetsTheName()
        {
            #region Arrange
            const string name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            #endregion

            #region Act

            initiaLearningUtility.Name = name;
            #endregion

            #region Assert
            Assert.AreEqual(50,name.Length);
            Assert.AreEqual(name, initiaLearningUtility.Name);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityNameIs101CharactersLongThrowsError()
        {
            #region Arrange
            const string name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            #endregion

            #region Act

            initiaLearningUtility.Name = name;
            #endregion

            #region Assert
            Assert.AreEqual(101, name.Length);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityNameHasAlphaNumericNameSetsIt()
        {
            #region Arrange
            const string name = "Wereldkaart 1";
            #endregion

            #region Act

            initiaLearningUtility.Name = name;
            #endregion

            #region Assert
            Assert.AreEqual(name, initiaLearningUtility.Name);
            #endregion

        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityNameHasIsNullThrowsError()
        {
            #region Arrange
            const string name = null;
            #endregion

            #region Act

            initiaLearningUtility.Name = name;
            #endregion

        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityNameHasIsEmptyThrowsError()
        {
            #region Arrange
            string name = String.Empty;
            #endregion

            #region Act

            initiaLearningUtility.Name = name;
            #endregion
        }
        #endregion

        #region DescriptionTests
        [TestMethod]
        public void LearningUtilityDescriptionIs101CharactersLongSetsTheDescription()
        {
            #region Arrange
            const string description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            #endregion

            #region Act

            initiaLearningUtility.Description = description;
            #endregion

            #region Assert
            Assert.AreEqual(101, description.Length);
            Assert.AreEqual(description, initiaLearningUtility.Description);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDescriptionIs1001CharactersLongThrowsError()
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

            initiaLearningUtility.Description = description;
            #endregion

            #region Assert
            Assert.AreEqual(1001, description.Length);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityDescriptionHasAlphaNumericDescriptionSetsIt()
        {
            #region Arrange
            const string description = "Wereldkaart 1 die ...";
            #endregion

            #region Act

            initiaLearningUtility.Description = description;
            #endregion

            #region Assert
            Assert.AreEqual(description, initiaLearningUtility.Description);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDescriptionHasIsNullThrowsError()
        {
            #region Act

            initiaLearningUtility.Description = null;
            #endregion

        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityDescriptionHasIsEmptyThrowsError()
        {
            #region Act

            initiaLearningUtility.Description = String.Empty;
            #endregion
        }
        #endregion

        #region LocationTests
        [TestMethod]
        public void LearningUtilityLocationSetsIt()
        {
            #region Act

            initiaLearningUtility.Location = initialLocation;

            #endregion

            #region Assert
            Assert.AreEqual(initialLocation.Name,initiaLearningUtility.Location.Name);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityLocationIsNullThrowsError()
        {
            #region Act

            initiaLearningUtility.Location = null;

            #endregion
        }
        #endregion

        #region PriceTests
        [TestMethod]
        public void LearningUtilityPriceIs50SetsIt()
        {
            #region Arrange

            var price = 50M;

            #endregion

            #region Act
            initiaLearningUtility.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual((decimal)price,initiaLearningUtility.Price);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityPriceIsInt50SetsIt()
        {
            #region Arrange

            int price = 50;

            #endregion

            #region Act
            initiaLearningUtility.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual((decimal)price, initiaLearningUtility.Price);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityPriceIsDecimalSetsIt()
        {
            #region Arrange

            decimal price = 0.87M;

            #endregion

            #region Act
            initiaLearningUtility.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual(price, initiaLearningUtility.Price);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LearningUtilityPriceIsNegativeThrowsError()
        {
            #region Arrange

            var price = Decimal.MinValue;

            #endregion

            #region Act
            initiaLearningUtility.Price = price;
            #endregion

        }
        [TestMethod]
        public void LearningUtilityPriceIsZeroSetsIt()
        {
            #region Arrange

            decimal price = Decimal.Zero;

            #endregion

            #region Act
            initiaLearningUtility.Price = price;
            #endregion

            #region Assert
            Assert.AreEqual((decimal)price, initiaLearningUtility.Price);
            #endregion
        }
        #endregion

        #region ArticleNumberTests
        [TestMethod]
        public void LearningUtilityArticleNumberIs50CharactersLongSetsTheArticleNumber()
        {
            #region Arrange
            const string articleNr = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            #endregion

            #region Act

            initiaLearningUtility.ArticleNumber = articleNr;
            #endregion

            #region Assert
            Assert.AreEqual(50, articleNr.Length);
            Assert.AreEqual(articleNr, initiaLearningUtility.ArticleNumber);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityArticleNumberIs101CharactersLongThrowsError()
        {
            #region Arrange
            const string articleNr = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            #endregion

            #region Act

            initiaLearningUtility.ArticleNumber = articleNr;
            #endregion

            #region Assert
            Assert.AreEqual(101, articleNr.Length);
            Assert.AreEqual(articleNr, initiaLearningUtility.ArticleNumber);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityArticleNumberHasAlphaNumericArticleNumberSetsIt()
        {
            #region Arrange
            const string articleNr = "FHZ.123";
            #endregion

            #region Act

            initiaLearningUtility.ArticleNumber = articleNr;
            #endregion

            #region Assert
            Assert.AreEqual(articleNr, initiaLearningUtility.ArticleNumber);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityArticleNumberHasIsNullSetsIt()
        {
            #region Arrange
            const string articleNr = null;
            #endregion

            #region Act

            initiaLearningUtility.ArticleNumber = articleNr;

            #endregion

            #region Assert
            Assert.AreEqual(articleNr, initiaLearningUtility.ArticleNumber);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityArticleNumberHasIsEmptySetsIt()
        {
            #region Arrange
            string articleNr = String.Empty;
            #endregion

            #region Act

            initiaLearningUtility.ArticleNumber = articleNr;

            #endregion

            #region Assert
            Assert.AreEqual(articleNr, initiaLearningUtility.ArticleNumber);
            #endregion
        }
        #endregion

        #region FieldOfStudyTests
        [TestMethod]
        public void LearningUtilityFieldOfStudySetsIt()
        {
            #region Act

            initiaLearningUtility.FieldsOfStudy.Add(initialFieldOfStudy);

            #endregion

            #region Assert
            Assert.AreEqual(initialFieldOfStudy,initiaLearningUtility.FieldsOfStudy.Single(f => f.Equals(initialFieldOfStudy)));
            #endregion
        }

        #endregion

        #region TargetGroupTests
        [TestMethod]
        public void LearningUtilityTargetGroupSetsIt()
        {
            #region Act

            initiaLearningUtility.TargetGroups.Add(initialTargetGroup);

            #endregion

            #region Assert
            Assert.AreEqual(initialTargetGroup,initiaLearningUtility.TargetGroups.Single(t => t.Equals(initialTargetGroup)));
            #endregion
        }
        #endregion

        #region CompanyTests
        [TestMethod]
        public void LearningUtilityCompanySetsIt()
        {
            #region Act

            initiaLearningUtility.Company = initialCompany;

            #endregion

            #region Assert
            Assert.AreEqual(initialCompany.Name, initiaLearningUtility.Company.Name);
            #endregion
        }
        #endregion

        #region Loanable
        [TestMethod]
        public void LearningUtilityLoanableSetsIt()
        {
            #region Act

            initiaLearningUtility.Loanable = true;

            #endregion

            #region Assert
            Assert.AreEqual(true, initiaLearningUtility.Loanable);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityUnloanableSetsIt()
        {
            #region Act

            initiaLearningUtility.Loanable = false;

            #endregion

            #region Assert
            Assert.AreEqual(false, initiaLearningUtility.Loanable);
            #endregion
        }
        #endregion

        #region PictureTests
        [TestMethod]
        public void LearningUtilityPictureUrlSetsIt()
        {
            #region Arrange
            const string pictureUrl = "/items/pictures/wereldbol.jpg";
            #endregion

            #region Act
            initiaLearningUtility.Picture = pictureUrl;
            #endregion

            #region Assert
            Assert.AreEqual(pictureUrl, initiaLearningUtility.Picture);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityPictureUrlIsEmptySetsIt()
        {
            #region Arrange
            string pictureUrl = string.Empty;
            #endregion

            #region Act
            initiaLearningUtility.Picture = pictureUrl;
            #endregion

            #region Assert
            Assert.AreEqual(pictureUrl, initiaLearningUtility.Picture);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LearningUtilityPictureURLIs251CharactersLongThrowsError()
        {
            #region Arrange

            const string url =
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + //100
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + //100
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab"; //51
            #endregion

            #region Act

            initiaLearningUtility.Picture = url;
            #endregion

            #region Assert
            Assert.AreEqual(251, url.Length);
            #endregion
        }
        [TestMethod]
        public void LearningUtilityPictureURLIs250CharactersLongSetsIt()
        {
            #region Arrange

            const string url =
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + //100
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + //100
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"; //50
            #endregion

            #region Act

            initiaLearningUtility.Picture = url;
            #endregion

            #region Assert
            Assert.AreEqual(250, url.Length);
            Assert.AreEqual(url, initiaLearningUtility.Picture);
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
            int amount = initiaLearningUtility.AmountAvailableForWeek(date1);

            //Assert
            Assert.AreEqual(0, amount);
        }

        [TestMethod]
        public void AmountAvailableForWeekWithoutReservationsReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtility.AmountAvailableForWeek(date2);

            //Assert
            Assert.AreEqual(5, amount);
        }

        [TestMethod]
        public void AmountAvailableForWeekWithCurrentWeekHigherThanReservedWeekReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtility.AmountAvailableForWeek(date2);

            //Assert
            Assert.AreEqual(5, amount);
        }
        #endregion

        #region AmountReservedForWeek
        [TestMethod]
        public void AmountReservedForWeekWithReservationsReturnsCorrectValue()
        {
            //Arrange
            initiaLearningUtility.Reservations.First().User = new Student();

            //Act
            int amount = initiaLearningUtility.AmountReservedForWeek(date1);

            //Assert
            Assert.AreEqual(5, amount);
        }

        [TestMethod]
        public void AmountReservedForWeekWithoutReservationsReturnsZero()
        {
            //Arrange
            initiaLearningUtility.Reservations.First().User = new Student();

            //Act
            int amount = initiaLearningUtility.AmountReservedForWeek(date2);

            //Assert
            Assert.AreEqual(0, amount);
        }

        [TestMethod]
        public void AmountReservedForWeekWithReservationsByLectorReturnsZero()
        {
            //Arrange
            initiaLearningUtility.Reservations.First().User = new Lector();

            //Act
            int amount = initiaLearningUtility.AmountReservedForWeek(date1);

            //Assert
            Assert.AreEqual(0, amount);
        }
        #endregion

        #region AmountBlockedForWeek
        [TestMethod]
        public void AmountBlockedForWeekWithReservationsReturnsCorrectValue()
        {
            //Arrange
            initiaLearningUtility.Reservations.First().User = new Lector();

            //Act
            int amount = initiaLearningUtility.AmountBlockedForWeek(date1);

            //Assert
            Assert.AreEqual(5, amount);
        }

        [TestMethod]
        public void AmountBlockedForWeekWithoutReservationsReturnsZero()
        {
            //Arrange
            initiaLearningUtility.Reservations.First().User = new Lector();

            //Act
            int amount = initiaLearningUtility.AmountBlockedForWeek(date2);

            //Assert
            Assert.AreEqual(0, amount);
        }

        [TestMethod]
        public void AmountBlockedForWeekWithReservationsByStudentReturnsZero()
        {
            //Arrange
            initiaLearningUtility.Reservations.First().User = new Student();

            //Act
            int amount = initiaLearningUtility.AmountBlockedForWeek(date1);

            //Assert
            Assert.AreEqual(0, amount);
        }
        #endregion

        #region AmountUnavailableForWeek
        [TestMethod]
        public void AmountUnavailableForWeekWithReservationsByStudentReturnsCorrectValue()
        {
            //Arrange
            initiaLearningUtility.Reservations.First().User = new Student();

            //Act
            int amount = initiaLearningUtility.AmountUnavailableForWeek(date1);

            //Assert
            Assert.AreEqual(10, amount);
        }

        [TestMethod]
        public void AmountUnavailableForWeekWithReservationsByLectorReturnsCorrectValue()
        {
            //Arrange
            initiaLearningUtility.Reservations.First().User = new Lector();

            //Act
            int amount = initiaLearningUtility.AmountUnavailableForWeek(date1);

            //Assert
            Assert.AreEqual(10, amount);
        }

        [TestMethod]
        public void AmountUnavailableForWeekWithoutReservationsReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtility.AmountUnavailableForWeek(date2);

            //Assert
            Assert.AreEqual(5, amount);
        }

        [TestMethod]
        public void AmountUnavailableForWeekWithCurrentWeekHigherThanReservedWeekReturnsCorrectValue()
        {
            //Act
            int amount = initiaLearningUtility.AmountUnavailableForWeek(date3);

            //Assert
            Assert.AreEqual(5, amount);
        }
        #endregion
        #endregion
    }
}
