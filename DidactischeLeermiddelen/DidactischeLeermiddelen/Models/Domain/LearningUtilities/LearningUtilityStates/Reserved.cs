using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class Reserved : LearningUtilityState
    {
        public Reserved(LearningUtility learningUtility) : base(learningUtility) { }

        public override void Block(User user)
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Blocked, base.LearningUtility));
        }

        public override void MakeAvailable()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Available, base.LearningUtility));
        }
    }
}