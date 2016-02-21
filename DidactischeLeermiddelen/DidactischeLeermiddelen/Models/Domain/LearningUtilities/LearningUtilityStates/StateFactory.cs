using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// A simple factory for creating the concrete LearningUtilityState classes
    /// </summary>
    public class StateFactory
    {
        /// <summary>
        /// This method creates a new concrete LearningUtilityState and returns it.
        /// If no matching type is found, this method returns null.
        /// </summary>
        /// <param name="type">The type of the state, can be: "Available", "Reserved", "Blocked", "HandedOut", "Late" or "Unavailable"</param>
        /// <param name="learningUtility">The LearningUtility instance that should contain the created state</param>
        /// <returns></returns>
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