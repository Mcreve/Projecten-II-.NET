using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.Domain.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> FindAll();
        Product FindById(int Id);
        void Add(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}