using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.ViewModels
{
    public class ProductGroupViewModel
    {
        public int ProductGroupId { get; private set; }
        public string Name { get; private set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; private set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public ProductGroupViewModel(ProductGroup productGroup)
        {
            ProductGroupId = productGroup.ProductGroupId;
            Name = productGroup.Name;
            Price = productGroup.Price;
            Description = productGroup.Description;
            Amount = productGroup.Amount;
        }
    }
}