using System;
using System.Collections.Generic;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class Student : User
    {
        #region Constructor

        public Student() : base()
        {
            
        }
        public Student(string firstName, string lastName, string emailAddress) : base(firstName, lastName, emailAddress)
        {

        } 
        #endregion
        #region Methods

        public override IQueryable<LearningUtilityDetails> GetLearningUtilities(
            ILearningUtilityDetailsRepository leanringUtilityDetailsRepository)
        {
            return leanringUtilityDetailsRepository.FindAll().Where(learningUtilityDetails => learningUtilityDetails.Loanable == true);
        }

        #endregion
    }
}