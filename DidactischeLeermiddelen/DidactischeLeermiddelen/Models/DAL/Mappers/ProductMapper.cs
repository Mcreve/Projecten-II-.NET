using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.DAL.Mappers
{
    public class ProductMapper : EntityTypeConfiguration<Product>
    {
        #region Constructor
        public ProductMapper()
        {
            ToTable("Product");
            HasKey(prod => prod.ProductId);
            HasRequired(prod => prod.Location).WithMany().Map(m => m.MapKey("LocationId")).WillCascadeOnDelete(false);

            Property(prod => prod.Availability).IsRequired();

        }
        #endregion
    }
}