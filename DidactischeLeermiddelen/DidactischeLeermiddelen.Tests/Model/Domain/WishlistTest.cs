using System;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain
{
    [TestClass]
    public class WishlistTest
    {
        private DummyDataContext context;
        private LearningUtilityDetails item1;
        private LearningUtilityDetails item2;
        private Wishlist wishlist;

        [TestInitialize]
        public void Initialize()
        {
            context = new DummyDataContext();
            item1 = context.LearningUtilityDetails1;
            item2 = context.LearningUtilityDetails2;
            wishlist = new Wishlist();
        }

        [TestMethod]
        public void NewWishlistStartsEmpty()
        {
            Assert.AreEqual(0, wishlist.NumberOfItems);
        }

        [TestMethod]
        public void WishlistItemsCanBeAdded()
        {
            //Act
            wishlist.AddItem(item1);
            wishlist.AddItem(item2);

            //Assert
            Assert.AreEqual(2, wishlist.NumberOfItems);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WishlistThrowsExceptionWhenAllreadyAdded()
        {
            //Act
            wishlist.AddItem(item1);
            wishlist.AddItem(item1);
        }

        [TestMethod]
        public void WishlistItemCanBeDeleted()
        {
            //Arrange
            wishlist.AddItem(item1);
            wishlist.AddItem(item2);

            //Act
            wishlist.RemoveItem(item1);

            //Assert
            Assert.AreEqual(1, wishlist.NumberOfItems);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void WishlistThrowsExceptionWhenRemoveDoesntFindItem()
        {
            //Arrange
            wishlist.RemoveItem(item2);
        }
    }
}
