using System.Data.Entity;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Interfaces;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.DAL.Repositories
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly LeermiddelenContext context;
        private readonly DbSet<ProductGroup> productGroups;
         
        public ProductGroupRepository(LeermiddelenContext context)
        {
            this.context = context;
            this.productGroups = context.ProductGroups;
        }
        public IQueryable<ProductGroup> FindAll()
        {
            return productGroups.OrderBy(productGroup => productGroup.Name);
        }

        public ProductGroup FindById(int id)
        {
            return productGroups.Include(productGroup => productGroup.Category)
                .FirstOrDefault(productGroup => productGroup.ProductGroupId == id);
        }

        public void Add(ProductGroup productGroup)
        {
            productGroups.Add(productGroup);
        }

        public void Delete(ProductGroup productGroup)
        {
            productGroups.Remove(productGroup);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}