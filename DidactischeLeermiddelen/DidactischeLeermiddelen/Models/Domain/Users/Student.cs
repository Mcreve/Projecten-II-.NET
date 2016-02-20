using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Properties;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class Student : User
    {
        public override IList<LearningUtilityDetails> GetLearningUtilities(ILearningUtilityDetailsRepository leanringUtilityDetailsRepository)
        {
            throw new NotImplementedException();
        }
    }
}