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
using System.Web;
using System.Web.Routing;
using DidactischeLeermiddelen.Models;
using DidactischeLeermiddelen.ViewModels;


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
        private Mock<ITargetGroupRepository> targetGroupRepository;
        private Mock<IFieldOfStudyRepository> fieldOfStudyRepository;
        private Mock<HttpRequestBase> request;
        private Mock<HttpContextBase> httpcontext;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange

            student = UserFactory.CreateUserWithUserType(UserType.Student);
            lector = UserFactory.CreateUserWithUserType(UserType.Lector);
            context = new DummyDataContext();
            itemRepository = new Mock<ILearningUtilityDetailsRepository>();
            targetGroupRepository = new Mock<ITargetGroupRepository>();
            fieldOfStudyRepository = new Mock<IFieldOfStudyRepository>();
            string searchInput = "bol";
            itemRepository.Setup(i => i.FindAll()).Returns(context.LearningUtilityDetailsList);
            itemRepository.Setup(i => i.Search(searchInput)).Returns(context.LearningUtilityDetailsList.Where(j => (j.Name).Contains(searchInput)).ToList());
            itemRepository.Setup(i => i.Search(searchInput)).Returns(context.LearningUtilityDetailsList.Where(j => (j.Description).Contains(searchInput)).ToList());
            itemRepository.Setup(i => i.FindBy(1)).Returns(context.LearningUtilityDetails1);
            itemRepository.Setup(i => i.FindBy(2)).Returns(context.LearningUtilityDetails2);
            itemRepository.Setup(i => i.FindBy(3)).Returns(context.LearningUtilityDetails3);
            catalogController = new CatalogController(itemRepository.Object, targetGroupRepository.Object, fieldOfStudyRepository.Object);

            request = new Mock<HttpRequestBase>();
            
            httpcontext = new Mock<HttpContextBase>();
            httpcontext.SetupGet(x => x.Request).Returns(request.Object);
            catalogController.ControllerContext = new ControllerContext(httpcontext.Object, new RouteData(), catalogController);
        }

        #region Index
        [TestMethod]
        public void IndexShowsCatalogForStudents()
        {

            //Arrange
            student = context.Student1;
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "" } });

            //Act
            ViewResult result = catalogController.Index(student,null, null, null, null, null, null, null) as ViewResult;
            IEnumerable <CatalogViewModel> catalog = result.ViewData.Model as IEnumerable<CatalogViewModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2,catalog.Count());
        }

        [TestMethod]
        public void IndexShowsCatalogForLectors()
        {

            //Arrange
            lector = context.Lector1;
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "" } });

            //Act
            ViewResult result = catalogController.Index(lector, null, null, null, null, null, null, null) as ViewResult;
            IEnumerable<CatalogViewModel> catalog = result.ViewData.Model as IEnumerable<CatalogViewModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, catalog.Count());
        }

        [TestMethod]
        public void IndexReturnPartialViewIfIsAjaxRequest()
        {
            //Arrange
            lector = context.Lector1;
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "XMLHttpRequest" } });

            //Act
            PartialViewResult result = catalogController.Index(lector, null, null, null, null, null, null, null) as PartialViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        #endregion

        #region Details 
        [TestMethod]
        public void ShowDetailsSelectedLearningUtility()
        {


            //Act
            ViewResult result = catalogController.Details(context.LearningUtilityDetails2.Id, null, null, null, null) as ViewResult;
            LearningUtilityDetailsViewModel learningUtilityDetails = result.ViewData.Model as LearningUtilityDetailsViewModel;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(context.LearningUtilityDetails2.Id, learningUtilityDetails.Id);
        }

        [TestMethod]
        public void ParameterCanNotBeNull()
        {
            //Act
            HttpNotFoundResult result = catalogController.Details(null, null, null, null, null) as HttpNotFoundResult;

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void ParameterCanNotBeZero()
        {

            //Act
            HttpNotFoundResult result = catalogController.Details(0, null, null, null, null) as HttpNotFoundResult;

            //Assert
            Assert.IsNotNull(result);

        }

        #endregion
    }

}