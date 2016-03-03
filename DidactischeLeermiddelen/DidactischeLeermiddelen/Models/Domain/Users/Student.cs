using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class Student : User
    {
        #region Constructor
        /// <summary>
        /// Constructor without parameters, calls the base constructor of User (superclass)
        /// </summary>
        public Student() : base()
        {
            
        }
        /// <summary>
        /// Constructor with 3 parameters, calls the base constructor of User (superclass)
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        public Student(string firstName, string lastName, string emailAddress) : base(firstName, lastName, emailAddress)
        {

        }
        #endregion

    }
}