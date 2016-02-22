using System;
using System.Collections.Generic;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class Lector : User
    {
        #region Constructor

        public Lector() : base()
        {
            
        }
        public Lector(string firstName, string lastName, string emailAddress) : base(firstName, lastName, emailAddress)
        {
        }   
        #endregion

        #region Methods

        public override IQueryable<LearningUtilityDetails> GetLearningUtilities(
            ILearningUtilityDetailsRepository learningUtilityDetailsRepository)
        {
            return learningUtilityDetailsRepository.FindAll();
        }

        #endregion
    }
}