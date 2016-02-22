using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// This class represents the Reserved state. This implies that the item is available at it's location
    /// and that a user reserved the item for use next week.
    /// </summary>
    public class Reserved : LearningUtilityState
    {
        #region Constructors
        /// <summary>
        /// This constructor calls the base constructor to set the LearningUtility instance.
        /// </summary>
        /// <param name="learningUtility">The instance of the LearningUtility that is owner of this state</param>
        public Reserved(LearningUtility learningUtility) : base(learningUtility) { }

        /// <summary>
        /// Constructor without parameters for EntityFramework functionality
        /// </summary>
        public Reserved() { }
        #endregion

        #region Methods
        /// <summary>
        /// When in this state a user can block this item when he has the right previliges.
        /// This method sets the ReservedBy property of LearningUtility to the passed user object.
        /// When this method is called the state will change to Blocked.
        /// </summary>
        /// <param name="user">The user that tries to block the item</param>
        public override void Block(User user)
        {
            base.LearningUtility.ReservedBy = user;
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Blocked, base.LearningUtility));
        }

        /// <summary>
        /// When in this state a user can make the item available.
        /// This method sets the ReservedBy property of LearningUtility to null
        /// and sets the state to Available.
        /// </summary>
        public override void MakeAvailable()
        {
            base.LearningUtility.ReservedBy = null;
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Available, base.LearningUtility));
        } 
        #endregion
    }
}