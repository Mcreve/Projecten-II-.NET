using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class HandedOut : LearningUtilityState
    {
        public HandedOut(LearningUtility learningUtility) : base(learningUtility) {}

        public override void Late()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Late, base.LearningUtility));
        }
    }
}