using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain
{
    public interface IUserRepository
    {
        User FindBy(string emailAdress);
        void SaveChanges();
        void Add(User user);
    }
}