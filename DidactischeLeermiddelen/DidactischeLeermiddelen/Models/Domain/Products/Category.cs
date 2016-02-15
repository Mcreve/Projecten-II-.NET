using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Enums;

namespace DidactischeLeermiddelen.Models.Domain.Products
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Display(Name = "Categorienaam")]
        public string Name { get; set; }
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        public Category()
        {
            ProductGroups = new List<ProductGroup>();
        }

        public Category(string name):this()
        {
            this.Name = name;
        }

        public void AddProductGroup(ProductGroup productGroup)
        {
            if (ProductGroups.FirstOrDefault(p => p.Name == productGroup.Name) == null)
            {
                productGroup.Category = this;
                ProductGroups.Add(productGroup);
            }
        }
        public ProductGroup FindProductGroupByName(string naam)
        {
            return ProductGroups.FirstOrDefault(p => p.Name == naam);
        }
    }
}
