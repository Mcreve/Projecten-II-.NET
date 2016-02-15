using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using DidactischeLeermiddelen.Models.Domain.Enums;
using DidactischeLeermiddelen.Models.Domain.Locations;

namespace DidactischeLeermiddelen.Models.Domain.Products
{
    public class ProductGroup
    {
        public int ProductGroupId { get; set; }
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Display(Name = "Omschrijving")]
        public string Description { get; set; }
        [Display(Name = "Prijs")]
        public decimal Price { get; set; }
        [Display(Name = "Foto")]
        public string Photo { get; set; }
        [Display(Name = "Uitleenbaar")]
        public Loanable Loanable { get; set; }
        [Display(Name = "Aantal")]
        public int Amount { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public ProductGroup()
        {
            Products = new List<Product>();
            Loanable = Loanable;
        }

        public ProductGroup(string name, string description, decimal price, Loanable loanable, string photo):this()
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Loanable = loanable;
            this.Photo = photo;
        }

        public void AddProduct(Product product)
        {
            product.ProductGroup = this;
            Products.Add(product);
            //Performance wise
            this.Amount ++;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is ProductGroup)
                if ((obj as ProductGroup).ProductGroupId == ProductGroupId)
                    return true;
            return false;
        }

        public override int GetHashCode()
        {
            return ProductGroupId;
        }


    }
}