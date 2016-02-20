using System;
using System.Collections.Generic;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class Lector : User
    {
        #region Methods

        public override IList<LearningUtilityDetails> GetLearningUtilities(
            ILearningUtilityDetailsRepository learningUtilityDetailsRepository)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}