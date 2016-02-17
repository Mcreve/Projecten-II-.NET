namespace DidactischeLeermiddelen.Models.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Customer FindBy(string customerName);
        void SaveChanges();
        void Add(Customer customer);
    }
}