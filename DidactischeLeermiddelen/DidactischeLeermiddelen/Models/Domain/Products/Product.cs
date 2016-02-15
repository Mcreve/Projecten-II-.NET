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

        public Product()
        {
            Availability = Availability.Available;
        }
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Product)
                if ((obj as Product).ProductId == ProductId)
                    return true;
            return false;
        }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }
}