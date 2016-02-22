using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// This class represents the HandedOut state. It implies that the object is handed out to a user.
    /// </summary>
    public class HandedOut : LearningUtilityState
    {
        #region Constructors
        /// <summary>
        /// This constructor calls the base constructor to set the LearningUtility instance.
        /// </summary>
        /// <param name="learningUtility">The instance of the LearningUtility that is owner of this state</param>
        public HandedOut(LearningUtility learningUtility) : base(learningUtility) { }

        /// <summary>
        /// Constructor without parameters for EntityFramework functionality
        /// </summary>
        public HandedOut() { }
        #endregion

        #region Methods
        /// <summary>
        /// When in this state a user can reserve the object when it's not allready reserved.
        /// This is checked by looking if the ReservedBy parameter contains a reference to a user object.
        /// When a reference is found the method throws an InvalidOperationException, else it sets the
        /// ReservedBy property of the LearningUtility to the user passed to the method.
        /// </summary>
        /// <param name="user">The user who tries to reserve the object</param>
        public override void Reserve(User user)
        {
            if (base.LearningUtility.ReservedBy != null)
                throw new InvalidOperationException();
            base.LearningUtility.ReservedBy = user;
        }

        /// <summary>
        /// When in this state a user can block the object when it's not allready blocked.
        /// This is checked by looking if the ReservedBy parameter contains a reference to a Lector object.
        /// When a reference is found the method throws an InvalidOperationException, else it sets the
        /// ReservedBy property of the LearningUtility to the user passed to the method.
        /// </summary>
        /// <param name="user">The user who tries to block the object</param>
        public override void Block(User user)
        {
            if (base.LearningUtility.ReservedBy != null && base.LearningUtility.ReservedBy.GetType() == typeof(Lector))
                throw new InvalidOperationException();
            base.LearningUtility.ReservedBy = user;
        }

        /// <summary>
        /// When the object is in this state and has a reference to a user object in the ReservedBy property
        /// of the LearningUtility, the object can be made available for reservation or blocking. This is done
        /// by setting the ReservedBy property to null. When no reference to a user object is found, this method
        /// throws an InvalidOperationException.
        /// </summary>
        public override void MakeAvailable()
        {
            if (base.LearningUtility.ReservedBy == null)
                throw new InvalidOperationException();
            base.LearningUtility.ReservedBy = null;
        }

        /// <summary>
        /// When the object is in this state, this method changes to objects state to Late.
        /// </summary>
        public override void Late()
        {
            base.LearningUtility.ToState(StateFactory.CreateState(StateType.Late, base.LearningUtility));
        } 
        #endregion
    }
}