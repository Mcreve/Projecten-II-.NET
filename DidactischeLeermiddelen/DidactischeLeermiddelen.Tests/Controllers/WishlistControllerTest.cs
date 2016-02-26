using System;
using System.Linq;
using System.Web.Mvc;
using DidactischeLeermiddelen.Controllers;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DidactischeLeermiddelen.Tests.Controllers
{
    [TestClass]
    public class WishlistControllerTest
    {
        private Wishlist wishlist;
        private WishlistController wishlistController;
        private DummyDataContext context;
        private Mock<ILearningUtilityDetailsRepository> itemRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            context = new DummyDataContext();
            itemRepository = new Mock<ILearningUtilityDetailsRepository>();
            itemRepository.Setup(i => i.FindAll()).Returns(context.LearningUtilityDetailsList);
            itemRepository.Setup(i => i.FindBy(1)).Returns(context.LearningUtilityDetails1);
            itemRepository.Setup(i => i.FindBy(2)).Returns(context.LearningUtilityDetails2);
            itemRepository.Setup(i => i.FindBy(3)).Returns(context.LearningUtilityDetails3);
            wishlistController = new WishlistController(itemRepository.Object);
            wishlist = new Wishlist();
            wishlist.AddLine(context.LearningUtilityDetails1);
        }

        #region Index
        [TestMethod]
        public void IndexShowsEmptyWishlistIfWishlistIsEmpty()
        {
            //Arrange
            Wishlist emptyWishlist = new Wishlist();

            //Act
            ViewResult result = wishlistController.Index(emptyWishlist) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EmptyWishlist", result.ViewName);
        }

        [TestMethod]
        public void IndexShowsContentsOfWishlistWhenWishlistNotEmpty()
        {
            //Act
            ViewResult result = wishlistController.Index(wishlist) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(String.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void IndexShowsWishlistWhenWishlistNotEmpty()
        {
            //Act
            ViewResult result = wishlistController.Index(wishlist) as ViewResult;
            
            //Assert
            Assert.AreEqual(1, wishlist.NumberOfItems);
        }
        #endregion

        #region Add
        [TestMethod]
        public void AddRedirectsToCatalog()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Add(3, wishlist) as RedirectToRouteResult;

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
            RedirectToRouteResult result = wishlistController.Add(3, wishlist) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(2, wishlist.NumberOfItems);
            itemRepository.Verify(l => l.FindBy(3), Times.Once);
        }
        #endregion

        #region Remove
        [TestMethod]
        public void RemoveRedirectsToIndex()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Remove(1, wishlist) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            itemRepository.Verify(l => l.FindBy(1), Times.Once);
        }

        [TestMethod]
        public void RemoveWillRemoveItemFromWishlist()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Remove(1, wishlist) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(0, wishlist.NumberOfItems);
            itemRepository.Verify(l => l.FindBy(1), Times.Once);
        }
        #endregion

        #region Plus
        [TestMethod]
        public void PlusRedirectsToIndex()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Plus(1, wishlist) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            itemRepository.Verify(l => l.FindBy(1), Times.Once);
        }

        [TestMethod]
        public void PlusWillIncreaseQuantity()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Plus(1, wishlist) as RedirectToRouteResult;
            WishlistLine line = wishlist.WishlistLines.First();

            //Assert
            Assert.AreEqual(2, line.Quantity);
        }
        #endregion

        #region Min
        [TestMethod]
        public void MinRedirectsToIndex()
        {
            //Act
            RedirectToRouteResult result = wishlistController.Min(1, wishlist) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            itemRepository.Verify(l => l.FindBy(1), Times.Once);
        }

        [TestMethod]
        public void MinWillDecreaseQuantity()
        {
            //Act
            wishlistController.Plus(1, wishlist);
            RedirectToRouteResult result = wishlistController.Min(1, wishlist) as RedirectToRouteResult;
            WishlistLine line = wishlist.WishlistLines.First();

            //Assert
            Assert.AreEqual(1, line.Quantity);
        }
        #endregion
    }
}
