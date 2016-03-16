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
        private Mock<IUserRepository> userRepository;
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
            userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.FindBy("Benjamin.vertonghen@student.hogent.be")).Returns(context.Student1);
            reservationController = new ReservationController(itemRepository.Object, userRepository.Object);
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

            wishlistViewModels.FirstOrDefault().AmountWanted = 5;
            wishlistViewModels.FirstOrDefault().Week = 12;

            //Arrange
            ViewResult result = reservationController.Add(lector, wishlistViewModels) as ViewResult;
            IEnumerable<WishlistViewModel> reservations = result.ViewData.Model as IEnumerable<WishlistViewModel>;

            //Assert
            Assert.AreEqual(1, reservations.Count());

        }

        /// <summary>
        /// De lector geeft een te groot aantal van het te blokkeren materiaal en op welke dagen hij het materiaal nodig heeft.
        /// Het systeem valideert deze input en geeft de gepaste foutmelding
        /// </summary>
        [TestMethod]
        public void LectorEntersInvalidAmountOfMaterial()
        {
            //Act
            wishlistViewModels.FirstOrDefault().AmountWanted = 7;
            wishlistViewModels.FirstOrDefault().Week = 12;

            //Arrange
            RedirectToRouteResult result = reservationController.Add(lector, wishlistViewModels) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(reservationController.TempData["error"]);
        }



        [TestMethod]
        /// <summary>
        /// De lector geeft geen aantal van het te blokkeren materiaal en op welke dagen hij het materiaal nodig heeft.
        /// Het systeem valideert deze input en geeft de gepaste foutmelding
        /// </summary>
        public void LectorEntersNoAmountOfMaterial()
        {
            wishlistViewModels.FirstOrDefault().AmountWanted = 0;
            wishlistViewModels.FirstOrDefault().Week = 12;

            //Arrange
            RedirectToRouteResult result = reservationController.Add(lector, wishlistViewModels) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(reservationController.TempData["error"]);
        }


        [TestMethod]
        public void IndexShowsEmptyReservationsIfReservationsAreEmpty()
        {

            //Act
            ViewResult result = reservationController.Index(student) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EmptyReservations", result.ViewName);
        }

        [TestMethod]
        public void IndexShowsReservations()
        {
            //Arrange
            reservation = new LearningUtilityReservation { Week = 11, Amount = 5, User = student };
            context.LearningUtility1.AddReservation(reservation);

            //Act
            ViewResult result = reservationController.Index(student) as ViewResult;
            IEnumerable<ReservationViewModel> rvm = result.ViewData.Model as IEnumerable<ReservationViewModel>;

            //Assert
            Assert.AreEqual(rvm.Count(), 1);
        }

        /// <summary>
        /// Het systeem detecteert dat onvoldoende aantallen van het materiaal reserveerbaarzijn in de geselecteerde week. Het systeem past de reservering(en) aan. 
        /// De bestaande reservaties worden aangepast op basis van LIFO-principe ( aantal wordt verminderd en indien aantal=0 dan wordt reservatie geschrapt)

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
    }
    }