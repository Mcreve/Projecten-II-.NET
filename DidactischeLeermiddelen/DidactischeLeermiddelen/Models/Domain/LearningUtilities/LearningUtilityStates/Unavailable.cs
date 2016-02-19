using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class Unavailable : LearningUtilityState
    {
        public Unavailable(LearningUtility learningUtility) : base(learningUtility) { }
    }
}