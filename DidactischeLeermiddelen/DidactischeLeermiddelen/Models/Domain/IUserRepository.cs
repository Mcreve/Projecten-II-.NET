using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain
{
    public interface IUserRepository
    {
        #region Methods
        User FindBy(string id);
        void SaveChanges();
        void Add(User user); 
        #endregion
    }
}