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
        IEnumerable<WishlistViewModel> reservationViewModels;
        private ReservationController reservationController;
        private DummyDataContext context;
        private Mock<ILearningUtilityRepository> itemRepository;
        private Mock<IReservationRepository> reservationRepository;
        private User student;
        private User lector;
        private Mock<HttpRequestBase> request;
        private Mock<HttpContextBase> httpcontext;
        private Reservation reservation;
      




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
            reservationRepository = new Mock<IReservationRepository>();
            reservationRepository.Setup(i => i.FindAll()).Returns(context.reservationList);
            reservationRepository.Setup(i => i.FindAllForUser("Benjamin.vertonghen@student.hogent.be")).Returns(context.reservationList);
            reservationController = new ReservationController(reservationRepository.Object, itemRepository.Object);
            reservationRepository.Setup(i => i.FindBy(0)).Returns(context.reservation1);
            request = new Mock<HttpRequestBase>();
            httpcontext = new Mock<HttpContextBase>();
            httpcontext.SetupGet(x => x.Request).Returns(request.Object);
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "" } });
            reservationController.ControllerContext = new ControllerContext(httpcontext.Object, new RouteData(), reservationController);
            student = context.Student1;
            lector = context.Lector1;
            student.Wishlist = new Wishlist();
            student.Wishlist.AddItem(context.LearningUtility1);
            wishlistViewModels = student.Wishlist.LearningUtilities.Select(learningUtility =>
                 new WishlistViewModel(learningUtility))
                  .ToList();
          ;

        }




   
        [TestMethod]
        public void IndexShowsStartDateNextWeek()


        {

            //Deze methode misschien beter in de wishlistControllerTest invoegen? Daar we de date van het Viewmodel moeten instellen op het begin van volgende week

        }


        [TestMethod]
        public void StudentReservesMaterial()
        {
            //Act

            wishlistViewModels.FirstOrDefault().AmountWanted = 2;
            wishlistViewModels.FirstOrDefault().Date = new DateTime(2016, 3, 16);



            //Arrange
            RedirectToRouteResult result = reservationController.Add(student, wishlistViewModels) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }


        [TestMethod]
        public void StudentInvalidAmountOfMaterial()
        {
            //Act
            wishlistViewModels.FirstOrDefault().AmountWanted = 7;
            wishlistViewModels.FirstOrDefault().Week = 12;

            //Arrange
            RedirectToRouteResult result = reservationController.Add(student, wishlistViewModels) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(reservationController.TempData["error"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }



        [TestMethod]
        public void StudentsNoAmountOfMaterial()
        {
            wishlistViewModels.FirstOrDefault().AmountWanted = 0;
            wishlistViewModels.FirstOrDefault().Week = 12;

            //Arrange
            RedirectToRouteResult result = reservationController.Add(student, wishlistViewModels) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(reservationController.TempData["error"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void IndexShowsEmptyReservationsIfReservationsAreEmpty()
        {

            //Act
            ViewResult result = reservationController.Index(lector) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EmptyReservations", result.ViewName);
        }

        [TestMethod]
        public void IndexShowsReservations()
        {
   
            //Act
            ViewResult result = reservationController.Index(student) as ViewResult;
            IEnumerable<ReservationViewModel> rvm = result.ViewData.Model as IEnumerable<ReservationViewModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, rvm.Count());
        }

      
        [TestMethod]
        public void DeleteExistingReservation()
        {

            //Arrange
            RedirectToRouteResult result = reservationController.Delete(0) as RedirectToRouteResult;


            //Assert

            Assert.AreEqual("Index", result.RouteValues["action"]);

        }
    }
    }