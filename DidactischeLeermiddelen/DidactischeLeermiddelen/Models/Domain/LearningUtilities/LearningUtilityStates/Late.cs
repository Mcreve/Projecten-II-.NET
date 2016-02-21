using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// This class represents the Late state. It implies that the LearningUtility is handed out to 
    /// a student or lector but this person didn't return the item in time.
    /// </summary>
    public class Late : LearningUtilityState
    {
        #region Constructors
        /// <summary>
        /// This constructor calls the base constructor to set the LearningUtility instance.
        /// </summary>
        /// <param name="learningUtility">The instance of the LearningUtility that is owner of this state</param>
        public Late(LearningUtility learningUtility) : base(learningUtility) { }

        /// <summary>
        /// Constructor without parameters for EntityFramework functionality
        /// </summary>
        public Late() { }
        #endregion

        #region Methods
        /// <summary>
        /// When in this state an item can be reserved when it's not allready reserved.
        /// This is checked by looking if the ReservedBy property of LearningUtility contains a reference or not.
        /// When the property allready conatains a reference, then an exception is thrown, else the property is
        /// set to the user passed by the parameter. This method doesn't change the state of the object.
        /// </summary>
        /// <param name="user">The user that tries to reserve the LearningUtility</param>
        public override void Reserve(User user)
        {
            if (base.LearningUtility.ReservedBy != null)
                throw new InvalidOperationException();
            base.LearningUtility.ReservedBy = user;
        }

        /// <summary>
        /// When in this state an item can be blocked when it's not allready blocked.
        /// This is checked by looking if the ReservedBy property of LearningUtility contains a reference to a Lector object or not.
        /// When the property allready conatains a reference, then an exception is thrown, else the property is
        /// set to the user passed by the parameter. This method doesn't change the state of the object.
        /// </summary>
        /// <param name="user">The user that tries to block the LearningUtility</param>
        public override void Block(User user)
        {
            if (base.LearningUtility.ReservedBy != null && base.LearningUtility.ReservedBy.GetType() == typeof(Lector))
                throw new InvalidOperationException();
            base.LearningUtility.ReservedBy = user;
        }

        /// <summary>
        /// When in this state an item can be made available when it's blocked or reserverd.
        /// This is checked by looking if the ReservedBy property of LearningUtility contains a reference or not.
        /// When the property contains a reference, then the property is set to null. Else this method throws an
        /// exception. This method doesn't change the state of the object.
        /// </summary>
        public override void MakeAvailable()
        {
            if (base.LearningUtility.ReservedBy == null)
                throw new InvalidOperationException();
            base.LearningUtility.ReservedBy = null;
        } 
        #endregion
    }
}