using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class Late : LearningUtilityState
    {
        public Late(LearningUtility learningUtility) : base(learningUtility) { }

    }
}