using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class StateFactory
    {
        public static LearningUtilityState CreateState(StateType type, LearningUtility learningUtility)
        {
            switch (type)
            {
                case StateType.Available:
                    return new Available(learningUtility);
                case StateType.Reserved:
                    return new Reserved(learningUtility);
                case StateType.Blocked:
                    return new Blocked(learningUtility);
                case StateType.HandedOut:
                    return new HandedOut(learningUtility);
                case StateType.Late:
                    return new Late(learningUtility);
                case StateType.Unavailable:
                    return new Unavailable(learningUtility);
                default:
                    return null;
            }
        }
    }
}