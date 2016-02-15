using System.Collections.Generic;

namespace DidactischeLeermiddelen.Models.Domain.Products
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductGroup> ProductGroups { get; set; } 
    }
}