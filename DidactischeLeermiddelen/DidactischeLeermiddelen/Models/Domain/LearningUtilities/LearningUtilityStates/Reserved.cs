using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    internal class Reserved : LearningUtilityState
    {
        internal Reserved(LearningUtility learningUtility) : base(learningUtility) { }

        internal override void HandOut()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.HandedOut, base.LearningUtility));
        }

        internal override void Block()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Blocked, base.LearningUtility));
        }

        internal override void MakeAvailable()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Available, base.LearningUtility));
        }
    }
}