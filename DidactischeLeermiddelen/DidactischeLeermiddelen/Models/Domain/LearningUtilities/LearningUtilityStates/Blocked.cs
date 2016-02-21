using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// This class represents the blocked state. This implies that the object is available at his location
    /// and that a user has blocked the object for use.
    /// </summary>
    public class Blocked : LearningUtilityState
    {
        #region Constructors
        /// <summary>
        /// This constructor calls the base constructor to set the LearningUtility instance.
        /// </summary>
        /// <param name="learningUtility">The instance of the LearningUtility that is owner of this state</param>
        public Blocked(LearningUtility learningUtility) : base(learningUtility) { }

        /// <summary>
        /// Constructor without parameters for EntityFramework functionality
        /// </summary>
        public Blocked() { }
        #endregion

        #region Methods
        /// <summary>
        /// When the object is in this state the object can be made available again.
        /// This method sets the ReservedBy property of LearningUtility to null and changes it's state
        /// to Available.
        /// </summary>
        public override void MakeAvailable()
        {
            base.LearningUtility.ReservedBy = null;
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Available, base.LearningUtility));
        } 
        #endregion
    }
}