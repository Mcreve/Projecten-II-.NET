using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class LearningUtilityState
    {
        protected LearningUtility LearningUtility { get; set; }

        public LearningUtilityState(LearningUtility learningUtility)
        {
            this.LearningUtility = learningUtility;
        }

        public virtual void Reserve(User user)
        {
            throw new InvalidOperationException();
        }

        public virtual void Block(User user)
        {
            throw new InvalidOperationException();
        }

        public virtual void Late()
        {
            throw new InvalidOperationException();
        }

        public virtual void MakeAvailable()
        {
            throw new InvalidOperationException();
        }

    }
}