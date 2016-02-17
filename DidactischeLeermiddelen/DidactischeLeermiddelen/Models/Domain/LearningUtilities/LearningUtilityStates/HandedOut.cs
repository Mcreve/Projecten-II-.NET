using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    internal class HandedOut : LearningUtilityState
    {
        internal HandedOut(LearningUtility learningUtility) : base(learningUtility) {}

        internal override void MakeAvailable()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Available, base.LearningUtility));
        }

        internal override void MakeUnavailable()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Unavailable, base.LearningUtility));
        }

        internal override void Late()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Late, base.LearningUtility));
        }
    }
}