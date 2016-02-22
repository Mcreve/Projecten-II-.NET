using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// This class represents the Unavailable state. It implies that the LearningUtility
    /// is in such poor condition that it can't be handed out for learning purposes.
    /// </summary>
    public class Unavailable : LearningUtilityState
    {
        #region Constructors
        /// <summary>
        /// This constructor calls the base constructor to set the LearningUtility instance.
        /// </summary>
        /// <param name="learningUtility">The instance of the LearningUtility that is owner of this state</param>
        public Unavailable(LearningUtility learningUtility) : base(learningUtility) { }

        /// <summary>
        /// Constructor without parameters for EntityFramework functionality
        /// </summary>
        public Unavailable() { } 
        #endregion
    }
}