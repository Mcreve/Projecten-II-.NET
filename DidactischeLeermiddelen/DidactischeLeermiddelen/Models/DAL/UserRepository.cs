using System.Data.Entity;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class UserRepository : IUserRepository
    {
        #region Constructors

        public UserRepository(LeermiddelenContext context)
        {
            this.context = context;
            userList = context.UserList;
        }

        #endregion

        #region Properties

        private readonly LeermiddelenContext context;
        private readonly DbSet<User> userList;

        #endregion

        #region Methods

        /// <summary>
        ///     Finds a user based on e-mail address
        /// </summary>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        public User FindBy(string emailAdress)
        {
            return userList.FirstOrDefault(u => u.EmailAddress == emailAdress);
        }

        /// <summary>
        ///     Saves the context
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        /// <summary>
        ///     Adds a user to the database/repository
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            userList.Add(user);
        }

        #endregion
    }
}