using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    internal class Late : LearningUtilityState
    {
        internal Late(LearningUtility learningUtility) : base(learningUtility) { }

        internal override void MakeAvailable()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Available, base.LearningUtility));
        }

        internal override void MakeUnavailable()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Unavailable, base.LearningUtility));
        }
    }
}