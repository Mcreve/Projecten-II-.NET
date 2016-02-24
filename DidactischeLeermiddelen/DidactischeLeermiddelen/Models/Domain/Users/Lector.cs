using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    /// <summary>
    /// Subclass of the User class
    /// </summary>
    public class Lector : User
    {

        #region Constructor
        /// <summary>
        /// Constructor without parameters, calls the base constructor of User (superclass)
        /// </summary>
        public Lector() : base()
        {
            
        }
        /// <summary>
        /// Constructor with 3 parameters, calls the base constructor of User (superclass)
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        public Lector(string firstName, string lastName, string emailAddress) : base(firstName, lastName, emailAddress)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Returns the entire list of viewable LearningUtilityDetails
        /// </summary>
        /// <param name="learningUtilityDetailsRepository"></param>
        /// <returns>list of viewable LearningUtilityDetails</returns>
        public override IQueryable<LearningUtilityDetails> GetLearningUtilities(
            ILearningUtilityDetailsRepository learningUtilityDetailsRepository)
        {
            return learningUtilityDetailsRepository.FindAll();
        }

        #endregion
    }
}