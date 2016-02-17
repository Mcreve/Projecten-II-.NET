using System.Data.Entity;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Interfaces;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.DAL.Repositories
{
    public class ProductRepository :IProductRepository
    {
        private readonly LeermiddelenContext context;
        private readonly DbSet<Product> products;

        public ProductRepository(LeermiddelenContext context)
        {
            this.context = context;
            this.products = context.Products;
        } 
        public IQueryable<Product> FindAll()
        {
            return products;
        }

        public Product FindById(int Id)
        {
            return products.Include(product => product.ProductGroup).FirstOrDefault(product => product.ProductId == Id);
        }

        public void Add(Product product)
        {
            products.Add(product);
        }

        public void Delete(Product product)
        {
            products.Remove(product);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}