using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.DAL.Mappers
{
    public class ProductGroupMapper : EntityTypeConfiguration<ProductGroup>
    {
        #region Constructor
        public ProductGroupMapper()
        {
            ToTable("ProductGroup");
            HasMany(productGroup => productGroup.Products)
                                    .WithRequired(prop => prop.ProductGroup)
                                    .Map(m => m.MapKey("ProductGroupId"))
                                    .WillCascadeOnDelete(false);

            Property(productGroup => productGroup.Name).IsRequired()
                                                       .HasMaxLength(100);
            Property(productGroup => productGroup.Loanable).IsRequired();
            Property(productGroup => productGroup.Description).HasMaxLength(200);



        }
        #endregion
    }
}