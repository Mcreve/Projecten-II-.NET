using System;
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
        private User user;
        private Mock<ILearningUtilityDetailsRepository> itemRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            context = new DummyDataContext();
            itemRepository = new Mock<ILearningUtilityDetailsRepository>();
            itemRepository.Setup(i => i.FindAll()).Returns(context.LearningUtilityDetailsList);
            itemRepository.Setup(i => i.FindBy(1)).Returns(context.LearningUtilityDetails1);
            itemRepository.Setup(i => i.FindBy(2)).Returns(context.LearningUtilityDetails2);
            itemRepository.Setup(i => i.FindBy(3)).Returns(context.LearningUtilityDetails3);
            wishlistController = new WishlistController();
            wishlist = new Wishlist();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
