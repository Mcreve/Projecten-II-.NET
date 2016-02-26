using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// This class represents the Available state. This implies that the object is
    /// available at its location and available for reservation or blocking.
    /// </summary>
    public class Available : LearningUtilityState
    {
        #region Constructors
        /// <summary>
        /// This constructor calls the base constructor to set the LearningUtility instance.
        /// </summary>
        /// <param name="learningUtility">The instance of the LearningUtility that is owner of this state</param>
        public Available(LearningUtility learningUtility) : base(learningUtility) { }

        /// <summary>
        /// Constructor without parameters for EntityFramework functionality
        /// </summary>
        public Available() { }
        #endregion

        #region Methods
        /// <summary>
        /// When in this state a user can block the object. This method sets
        /// the ReservedBy property with the passed parameter and changes the object's state to Blocked.
        /// </summary>
        /// <param name="user">The user that wants to block the object</param>
        public override void Block(User user)
        {
            base.LearningUtility.ReservedBy = user;
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Blocked, base.LearningUtility));
        }

        /// <summary>
        /// When in this state a user can block the object. This method sets
        /// the ReservedBy property with the passed paramter, the TimeReserved property to Now and changes the object's state to Reserved.
        /// </summary>
        /// <param name="user">The user that wants to reserve the object</param>
        public override void Reserve(User user)
        {
            base.LearningUtility.TimeReserved = DateTime.Now;
            base.LearningUtility.ReservedBy = user;
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Reserved, base.LearningUtility));
        }
        #endregion
    }
}