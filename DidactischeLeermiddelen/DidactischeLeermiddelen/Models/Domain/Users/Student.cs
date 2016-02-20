using System;
using System.Collections.Generic;
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

        public override IList<LearningUtilityDetails> GetLearningUtilities(
            ILearningUtilityDetailsRepository leanringUtilityDetailsRepository)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}