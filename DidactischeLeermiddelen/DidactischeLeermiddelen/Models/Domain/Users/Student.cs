using System;
using System.Collections.Generic;
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

        #region Methods
        /// <summary>
        /// Returns the entire list of viewable LearningUtilityDetails which are Loanable
        /// </summary>
        /// <param name="leanringUtilityDetailsRepository"></param>
        /// <returns>List of viewable LearningUtilityDetails which are Loanable</returns>
        public override IQueryable<LearningUtilityDetails> GetLearningUtilities(
            ILearningUtilityDetailsRepository leanringUtilityDetailsRepository)
        {
            return leanringUtilityDetailsRepository.FindAll().Where(learningUtilityDetails => learningUtilityDetails.Loanable == true);
        }

        #endregion
    }
}