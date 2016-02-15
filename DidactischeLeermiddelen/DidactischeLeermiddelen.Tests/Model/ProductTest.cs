using System;
using DidactischeLeermiddelen.Models.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DidactischeLeermiddelen.Models.Domain.Enums;

namespace DidactischeLeermiddelen.Tests.Model
{
    [TestClass]
    public class ProductTest
    {
        private Product product1;

        [TestInitialize]
        public void ProductTestInializer()
        {
            product1 = new Product();
            
        }

        [TestMethod]
        public void DefaultConstructorCreatesProductAndSetsItAvailable()
        {
            Assert.IsNotNull(product1);
            Assert.AreEqual(Availability.Available, product1.Availability);
        }
    }
}
