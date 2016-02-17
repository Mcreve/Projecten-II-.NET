using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.DAL.Mappers
{
    public class CategoryMapper : EntityTypeConfiguration<Category>
    {
        #region Constructor
        public CategoryMapper()
        {
            ToTable("Category");
            HasMany(cat => cat.ProductGroups)
                              .WithRequired(cat => cat.Category)
                              .Map(m => m.MapKey("CategoryId"))
                              .WillCascadeOnDelete(false);
            Property(cat => cat.Name).IsRequired()
                                     .HasMaxLength(100);
                       
        }
        #endregion
    }
}