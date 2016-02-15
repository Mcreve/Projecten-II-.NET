using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> FindAll();
        Category FindById(int id);
        void Add(Category category);
        void Delete(Category category);
        void SaveChanges();
    }
}