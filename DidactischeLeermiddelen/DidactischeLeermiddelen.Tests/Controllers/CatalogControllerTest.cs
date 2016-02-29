using System;
using System.Linq;
using System.Web.Mvc;
using DidactischeLeermiddelen.Controllers;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using System.Collections.Generic;
using DidactischeLeermiddelen.Models;


namespace DidactischeLeermiddelen.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTest
    {
        
        private CatalogController catalogController;
        private DummyDataContext context;
        private Mock<ILearningUtilityDetailsRepository> itemRepository;
        private User student;
        private User lector;
        private List<LearningUtilityDetails> searchResults;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange

            student = UserFactory.CreateUserWithUserType(UserType.Student);
            lector = UserFactory.CreateUserWithUserType(UserType.Lector);
            context = new DummyDataContext();
            itemRepository = new Mock<ILearningUtilityDetailsRepository>();
            string searchInput = "bol";
            itemRepository.Setup(i => i.FindAll()).Returns(context.LearningUtilityDetailsList);
            itemRepository.Setup(i => i.Search(searchInput)).Returns(context.LearningUtilityDetailsList.Where(j => (j.Name).Contains(searchInput)).ToList());
            itemRepository.Setup(i => i.Search(searchInput)).Returns(context.LearningUtilityDetailsList.Where(j => (j.Description).Contains(searchInput)).ToList());
            itemRepository.Setup(i => i.FindBy(1)).Returns(context.LearningUtilityDetails1);
            itemRepository.Setup(i => i.FindBy(2)).Returns(context.LearningUtilityDetails2);
            itemRepository.Setup(i => i.FindBy(3)).Returns(context.LearningUtilityDetails3);
            catalogController = new CatalogController(itemRepository.Object);
            


        }

        #region Index
        [TestMethod]
        public void IndexShowsCatalogForStudents()
        {

            //Arrange
            student = context.Student1;

            //Act
            ViewResult result = catalogController.Index(student) as ViewResult;
            IEnumerable <CatalogViewModel> catalog = result.ViewData.Model as IEnumerable<DidactischeLeermiddelen.Models.CatalogViewModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2,catalog.Count());
        }

        [TestMethod]
        public void IndexShowsCatalogForLectors()
        {

            //Arrange
            lector = context.Lector1;

            //Act
            ViewResult result = catalogController.Index(lector) as ViewResult;
            IEnumerable<CatalogViewModel> catalog = result.ViewData.Model as IEnumerable<DidactischeLeermiddelen.Models.CatalogViewModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, catalog.Count());
        }

        #endregion

        #region Details 
        [TestMethod]
        public void ShowDetailsSelectedLearningUtility()
        {


            //Act
            ViewResult result = catalogController.Details(context.LearningUtilityDetails2.Id) as ViewResult;
            LearningUtilityDetails catalog = result.ViewData.Model as DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityDetails;

            //Assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterCanNotBeNull()
        {


            //Act
            ViewResult result = catalogController.Details(null) as ViewResult;

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterCanNotBeZero()
        {

            //Act
            ViewResult result = catalogController.Details(0) as ViewResult;

        }

        #endregion

        #region Search
        [TestMethod]
        public void SearchShowsResultsWithMatchingName()
        {

            
            //Act
            ViewResult result = catalogController.Search("steen") as ViewResult;
            IEnumerable<CatalogViewModel> catalog = result.ViewData.Model as IEnumerable<DidactischeLeermiddelen.Models.CatalogViewModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, catalog.Count());
            Assert.AreEqual(2, catalog.FirstOrDefault(i => i.Name.Equals("Dobbelsteen schatkist 162-delig")).Id);
        }

        
        [TestMethod]
        public void SearchShowsResultWithMatchingDescription()
        {


            //Act
            ViewResult result = catalogController.Search("Spelbord") as ViewResult;
            IEnumerable<CatalogViewModel> catalog = result.ViewData.Model as IEnumerable<DidactischeLeermiddelen.Models.CatalogViewModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, catalog.Count());
        }
        [TestMethod]
        public void SearchShowsMultipleResultsWithMatchingDescription()
        {

            //Act
            ViewResult result = catalogController.Search("met") as ViewResult;
            IEnumerable<CatalogViewModel> catalog = result.ViewData.Model as IEnumerable<DidactischeLeermiddelen.Models.CatalogViewModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, catalog.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoResultsFoundException()
        {


            //Act
            ViewResult result = catalogController.Search("Bal") as ViewResult;

        }
        #endregion
    }

}