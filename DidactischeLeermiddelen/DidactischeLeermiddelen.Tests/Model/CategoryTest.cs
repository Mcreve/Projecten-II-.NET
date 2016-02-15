using System;
using DidactischeLeermiddelen.Models.Domain.Locations;
using DidactischeLeermiddelen.Models.Domain.Products;
using DidactischeLeermiddelen.Tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model
{
    [TestClass]
    public class CategoryTest
    {
        private DummyDataContext context;
        private Category globalCategory;

        [TestInitialize]
        public void CategoryTestInitialize()
        {
            context = new DummyDataContext();
            globalCategory = new Category("Global Cat.");
        }

        [TestMethod]
        public void ConstructorWithParamsCreatesCategory()
        {
            Category newCategory = new Category("wiskunde");
            Assert.AreEqual("wiskunde",newCategory.Name);
        }
        [TestMethod]
        public void AddProductGroupsToCategory()
        {
            Category testCategory = new Category("Test Category");
            testCategory.AddProductGroup(context.ProductGroup1);
            var addedCat = testCategory.FindProductGroupByName(context.ProductGroup1.Name);
            Assert.AreEqual(context.ProductGroup1.Name, addedCat.Name);
        }
        [TestMethod]
        public void CategoryFindsProductGroup()
        {
            var result = context.Category2.FindProductGroupByName(context.ProductGroup2.Name);
            Assert.AreEqual(context.ProductGroup2,result);
        }

    }
}
