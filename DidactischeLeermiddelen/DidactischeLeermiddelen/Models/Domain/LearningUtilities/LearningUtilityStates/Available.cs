using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    internal class Available : LearningUtilityState
    {
        internal Available(LearningUtility learningUtility) : base(learningUtility) { }

        internal override void Block()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Blocked, base.LearningUtility));
        }

        internal override void Reserve()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Reserverd, base.LearningUtility));
        }

        internal override void MakeUnavailable()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Unavailable, base.LearningUtility));
        }
    }
}