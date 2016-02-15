using System.Linq;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Customer FindById(string id);
        Customer FindByEmail(string email);
        IQueryable<Customer> FindAll();
        void SaveChanges();
        void Add(Customer customer);
    }
}