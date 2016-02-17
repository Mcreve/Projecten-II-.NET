using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    internal class Unavailable : LearningUtilityState
    {
        internal Unavailable(LearningUtility learningUtility) : base(learningUtility) { }

        internal override void MakeAvailable()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Available, base.LearningUtility));
        }
    }
}