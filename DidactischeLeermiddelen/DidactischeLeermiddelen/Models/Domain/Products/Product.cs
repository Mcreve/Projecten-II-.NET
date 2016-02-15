using DidactischeLeermiddelen.Models.Domain.Enums;
using DidactischeLeermiddelen.Models.Domain.Locations;

namespace DidactischeLeermiddelen.Models.Domain.Products
{
    public class Product
    {
        public int ProductId { get; set; }
        public Availability Availability { get; set; }
        public Location Location { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }

  
    }
}