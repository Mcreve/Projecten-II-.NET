using System.Collections.Generic;
using System.Security.Policy;
using DidactischeLeermiddelen.Models.Domain.Enums;

namespace DidactischeLeermiddelen.Models.Domain.Products
{
    public class ProductGroup
    {
        public int ProductGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public Loanable Loanable { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}