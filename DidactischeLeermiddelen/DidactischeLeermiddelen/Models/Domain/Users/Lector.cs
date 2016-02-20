using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class Lector : User
    {
        public override IList<LearningUtilityDetails> GetLearningUtilities(ILearningUtilityDetailsRepository learningUtilityDetailsRepository)
        {
            throw new NotImplementedException();
        }
    }
}