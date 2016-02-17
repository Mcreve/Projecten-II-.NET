using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    internal class LearningUtilityState
    {
        protected LearningUtility LearningUtility { get; set; }

        internal LearningUtilityState(LearningUtility learningUtility)
        {
            this.LearningUtility = learningUtility;
        }

        internal virtual void Reserve()
        {
            throw new NotImplementedException();
        }

        internal virtual void Block()
        {
            throw new NotImplementedException();
        }

        internal virtual void HandOut()
        {
            throw new NotImplementedException();
        }

        internal virtual void Late()
        {
            throw new NotImplementedException();
        }

        internal virtual void MakeAvailable()
        {
            throw new NotImplementedException();
        }

        internal virtual void MakeUnavailable()
        {
            throw new NotImplementedException();
        }
    }
}