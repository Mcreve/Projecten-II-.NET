using System;
using System.Linq;
using System.Web.Mvc;
using DidactischeLeermiddelen.Controllers;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Routing;

namespace DidactischeLeermiddelen.Tests.Controllers
{
    [TestClass]
    public class WishlistControllerTest
    {

        private WishlistController wishlistController;
        private DummyDataContext context;
        private Mock<ILearningUtilityRepository> itemRepository;
        private Mock<IUserRepository> userRepository;
        private User student;
        private User lector;
        private Wishlist wishlist;
        private Mock<HttpRequestBase> request;
        private Mock<HttpContextBase> httpcontext;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            request = new Mock<HttpRequestBase>();
            httpcontext = new Mock<HttpContextBase>();
            httpcontext.SetupGet(x => x.Request).Returns(request.Object);
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "" } });
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
            wishlistController = new WishlistController(itemRepository.Object, userRepository.Object);
            wishlistController.ControllerContext = new ControllerContext(httpcontext.Object, new RouteData(), wishlistController);
            wishlist = new Wishlist();
            wishlist.AddItem(context.LearningUtility1);
            student.Wishlist = wishlist;

        }

        #region Index
        [TestMethod]
        public void IndexShowsEmptyWishlistIfWishlistIsEmpty()
        {
            //Arrange
            Wishlist emptyWishlist = new Wishlist();
            student.Wishlist = emptyWishlist;


            //Act
            ViewResult result = wishlistController.Index(student) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EmptyWishlist", result.ViewName);
        }

        [TestMethod]
        public void IndexShowsContentsOfWishlistWhenWishlistNotEmpty()
        {
            //Act
            ViewResult result = wishlistController.Index(student) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(String.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void IndexShowsWishlistWhenWishlistNotEmpty()
        {
            //Act
            ViewResult result = wishlistController.Index(student) as ViewResult;

            //Assert
            Assert.AreEqual(1, student.Wishlist.NumberOfItems);
        }
        #endregion

        #region Add
        [TestMethod]
        public void AddRedirectsToCatalog()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Add(3, student) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Catalog", result.RouteValues["controller"]);
            itemRepository.Verify(l => l.FindBy(3), Times.Once);
        }

        [TestMethod]
        public void AddWillAddItemToWishlist()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Add(3, student) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(2, student.Wishlist.NumberOfItems);
            itemRepository.Verify(l => l.FindBy(3), Times.Once);
        }
        #endregion

        #region Remove
        [TestMethod]
        public void RemoveRedirectsToIndex()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Remove(1, student) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            itemRepository.Verify(l => l.FindBy(1), Times.Once);
        }

        [TestMethod]
        public void RemoveWillRemoveItemFromWishlist()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Remove(1, student) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(0, student.Wishlist.NumberOfItems);
            itemRepository.Verify(l => l.FindBy(1), Times.Once);
        }

        public void ReturnPartialViewIfIsAjaxRequest()
        {
            //Arrange
            
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "XMLHttpRequest" } });

            //Act
            PartialViewResult result = wishlistController.Index(student) as PartialViewResult;

            //Assert
            Assert.IsNotNull(result);
        }


        #endregion
    }
}
