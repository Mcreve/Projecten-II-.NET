using DidactischeLeermiddelen.Controllers;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using DidactischeLeermiddelen.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace DidactischeLeermiddelen.Tests.Controllers
{
    [TestClass]
    public class ReservationControllerTest
    {
        IEnumerable<WishlistViewModel> wishlistViewModels;
        private ReservationController reservationController;
        private DummyDataContext context;
        private Mock<ILearningUtilityRepository> itemRepository;
        private User student;
        private User lector;
        private Mock<HttpRequestBase> request;
        private Mock<HttpContextBase> httpcontext;
        private Wishlist wishlist;
        private LearningUtilityReservation reservation;


        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange

            student = UserFactory.CreateUserWithUserType(UserType.Student);
            lector = UserFactory.CreateUserWithUserType(UserType.Lector);
            context = new DummyDataContext();
            itemRepository = new Mock<ILearningUtilityRepository>();
            itemRepository.Setup(i => i.FindAll()).Returns(context.LearningUtilityList);
            itemRepository.Setup(i => i.FindBy(1)).Returns(context.LearningUtility1);
            itemRepository.Setup(i => i.FindBy(2)).Returns(context.LearningUtility2);
            itemRepository.Setup(i => i.FindBy(3)).Returns(context.LearningUtility3);
            reservationController = new ReservationController(itemRepository.Object);
            request = new Mock<HttpRequestBase>();
            httpcontext = new Mock<HttpContextBase>();
            httpcontext.SetupGet(x => x.Request).Returns(request.Object);
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "" } });
            reservationController.ControllerContext = new ControllerContext(httpcontext.Object, new RouteData(), reservationController);
            student = context.Student1;
            lector = context.Lector1;
            wishlist = new Wishlist();
            wishlist.AddItem(context.LearningUtility1);
            wishlistViewModels =
               wishlist.LearningUtilities.Select(learningUtility =>
                  new WishlistViewModel(learningUtility))
                   .ToList();


        }




        /// <summary>
        /// Het systeem toont de startdatum van de volgende week 
        /// </summary>
        [TestMethod]
        public void IndexShowsStartDateNextWeek()


        {

            //Deze methode misschien beter in de wishlistControllerTest invoegen? Daar we de date van het Viewmodel moeten instellen op het begin van volgende week

        }

        /// <summary>
        /// De lector geeft aantallen van het te blokkeren materiaal en geeft aan op welke dagen hij het materiaal nodig heeft.
        /// Het systeem valideert deze input en blokkeert het materiaal
        /// </summary>
        [TestMethod]
        public void LectorBlocksMaterial()
        {
            //Act
            reservation = new LearningUtilityReservation { Week = 12, Amount = 3, User = lector };
            context.LearningUtility1.LearningUtilityReservations.Add(reservation);

            //Arrange
            ViewResult result = reservationController.Add(lector, wishlistViewModels) as ViewResult;
            WishlistViewModel reservations = result.ViewData.Model as WishlistViewModel;

            //Assert
            Assert.AreEqual(reservations.Week, 12);
            Assert.AreEqual(reservations.AmountBlocked, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LectorEntersInvalidAmountOfMaterial()
        {
            //Act
            reservation = new LearningUtilityReservation { Week = 11, Amount = 6, User = lector };
            context.LearningUtility1.LearningUtilityReservations.Add(reservation);

            //Arrange
            RedirectToRouteResult result = reservationController.Add(lector, wishlistViewModels) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(reservationController.TempData["error"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LectorEntersInvalidDay()
        {
            reservation = new LearningUtilityReservation { Week = 10, Amount = 3, User = lector };
            context.LearningUtility1.LearningUtilityReservations.Add(reservation);

            //Arrange
            RedirectToRouteResult result = reservationController.Add(lector, wishlistViewModels) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(reservationController.TempData["error"]);
        }



        /// <summary>
        /// Het systeem detecteert dat onvoldoende aantallen van het materiaal reserveerbaarzijn in de geselecteerde week. Het systeem past de reservering(en) aan. 
        /// De bestaande reservaties worden aangepast op basis van LIFO-principe ( aantal wordt verminderd en indien aantal=0 dan wordt reservatie gesc
        /// apt)

        /// </summary>
        [TestMethod]
        public void EditExistingReservation()
        {

            //Act
            reservation = new LearningUtilityReservation { Week = 11, Amount = 5, User = lector };
            context.LearningUtility1.LearningUtilityReservations.Add(reservation);

            //Arrange
            ViewResult result = reservationController.Add(lector, wishlistViewModels) as ViewResult;
            WishlistViewModel reservations = result.ViewData.Model as WishlistViewModel;

            //Assert
            Assert.AreEqual(reservations.Week, 12);
            Assert.AreEqual(reservations.AmountBlocked, 5);
            Assert.AreEqual(context.LearningUtility1.AmountReservedForWeek((new DateTime(2016, 3, 14))), 1);

        }

        [TestMethod]
        public void NotifyUserOfChangeInReservation()
        {
            //Het systeem stuurt een e-mail bericht naar de persoon, of personen, wiens reservatie werd aangepast 

        }

        /// <summary>
        /// De lector kan op elk moment de reservatie info van een materiaal opvragen voor de geselecteerde week 
        /// Het systeem toont de materiaal info
        /// </summary>
        [TestMethod]
        public void GetLearningUtilityReservationDetailsForSelectedWeek()
        {
            //Dit is bij ons al het geval? of moet dit nog meer in detail?
        }

        /// <summary>
        /// De lector kan op elk moment de reservatie info van een gebruiker die een reservatie heeft. voor de geselecteerde week. 
        /// Het systeem toont de naam student/lector, aantal gereserveerd/geblokkeerd. 
        /// Indien materiaal geblokkeerd door lector dan zijn ook de weekdagen blokkering zichtbaar.

        /// </summary>
        [TestMethod]
        public void GetUserDetailsForSelectedWeek()
        {
            //TODO
        }

        /// <summary>
        /// Het systeem toont de gereserveerde en reserveerbare aantallen van dit materiaal voor de komende weken.
        /// </summary>
        public void ShowReservationsSummary()

        {
            //TODO
        }

    }

}

