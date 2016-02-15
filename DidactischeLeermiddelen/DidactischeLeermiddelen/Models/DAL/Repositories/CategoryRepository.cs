using System.Data.Entity;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Interfaces;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Global Scope
        private readonly LeermiddelenContext context;
        private readonly DbSet<Category> categories;

        public CategoryRepository(LeermiddelenContext context)
        {
            this.context = context;
            this.categories = context.Categories;
        }
        #endregion


        public IQueryable<Category> FindAll()
        {
            return categories.OrderBy(category => category.Name);
        }

        public Category FindById(int id)
        {
            return categories.Find(id);
        }

        public void Add(Category category)
        {
            categories.Add(category);
        }

        public void Delete(Category category)
        {
            categories.Remove(category);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}