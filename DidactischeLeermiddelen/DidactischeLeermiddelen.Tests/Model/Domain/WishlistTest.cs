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
            Assert.AreEqual(0, wishlist.WishlistLines.Count());
        }

        [TestMethod]
        public void WishlistItemsCanBeAdded()
        {
            //Act
            wishlist.AddLine(item1);
            wishlist.AddLine(item2);

            //Assert
            Assert.AreEqual(2, wishlist.NumberOfItems);
            Assert.AreEqual(1, wishlist.WishlistLines.First(l => l.LearningUtility.Equals(item1)).Quantity);
            Assert.AreEqual(1, wishlist.WishlistLines.First(l => l.LearningUtility.Equals(item2)).Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WishlistThrowsExceptionWhenAllreadyAdded()
        {
            //Act
            wishlist.AddLine(item1);
            wishlist.AddLine(item1);
        }

        [TestMethod]
        public void WishlistItemCanBeDeleted()
        {
            //Arrange
            wishlist.AddLine(item1);
            wishlist.AddLine(item2);

            //Act
            wishlist.RemoveLine(item1);

            //Assert
            Assert.AreEqual(1, wishlist.NumberOfItems);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void WishlistThrowsExceptionWhenRemoveDoesntFindItem()
        {
            //Arrange
            wishlist.RemoveLine(item2);
        }

        [TestMethod]
        public void WishlistIncreasesQuantityItem()
        {
            //Arrange
            wishlist.AddLine(item1);
            
            //Act
            wishlist.IncreaseQuantity(item1.Id);

            //Assert
            Assert.AreEqual(2, wishlist.WishlistLines.First(l => l.LearningUtility.Equals(item1)).Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void WishlistIncreaseQuantityThrowsExceptionWhenNoItemFound()
        {
            //Act
            wishlist.IncreaseQuantity(item1.Id);
        }
        [TestMethod]
        public void WishlistDecreasesQuantityItem()
        {
            //Arrange
            wishlist.AddLine(item1);
            wishlist.IncreaseQuantity(item1.Id);

            //Act
            wishlist.DecreaseQuantity(item1.Id);

            //Assert
            Assert.AreEqual(1, wishlist.WishlistLines.First(l => l.LearningUtility.Equals(item1)).Quantity);
        }

        [TestMethod]
        public void WishlistDecreasesQuantityItemBelowOneRemovesLine()
        {
            //Arrange
            wishlist.AddLine(item1);

            //Act
            wishlist.DecreaseQuantity(item1.Id);

            //Assert
            Assert.IsNull(wishlist.WishlistLines.FirstOrDefault(l => l.LearningUtility.Equals(item1)));
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void WishlistDecreaseQuantityThrowsExceptionWhenNoItemIsFound()
        {
            //Act
            wishlist.DecreaseQuantity(item1.Id);
        }
    }
}
