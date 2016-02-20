using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class Available : LearningUtilityState
    {
        public Available(LearningUtility learningUtility) : base(learningUtility) { }

        public override void Block(User user)
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Blocked, base.LearningUtility));
        }

        public override void Reserve(User user)
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Reserved, base.LearningUtility));
        }
    }
}