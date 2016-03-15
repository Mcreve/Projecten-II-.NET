﻿using System;
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
        private Mock<ILearningUtilityRepository> itemRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            context = new DummyDataContext();
            itemRepository = new Mock<ILearningUtilityRepository>();
            itemRepository.Setup(i => i.FindAll()).Returns(context.LearningUtilityList);
            itemRepository.Setup(i => i.FindBy(1)).Returns(context.LearningUtility1);
            itemRepository.Setup(i => i.FindBy(2)).Returns(context.LearningUtility2);
            itemRepository.Setup(i => i.FindBy(3)).Returns(context.LearningUtility3);
            wishlistController = new WishlistController(itemRepository.Object);
            wishlist = new Wishlist();
            wishlist.AddItem(context.LearningUtility1);
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
    }
}
