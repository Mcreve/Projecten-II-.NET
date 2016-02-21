using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// An instance of this class represents a single object that is referenced to LearningUtilityDetails object.
    /// It contains the state of the item and the user that has lend, reserved or blocked this item.
    /// This class should always be used to make the method calls to it's current state. In no circomstances should
    /// a concrete state method be called outside of this class.
    /// </summary>
    public class LearningUtility
    {
        #region Fields
        private LearningUtilityState currentState;
        #endregion

        #region Properties
        /// <summary>
        /// The state this instance is currently in
        /// </summary>
        public virtual LearningUtilityState CurrentState { get { return currentState; } }
        /// <summary>
        /// Property for EntityFramework functionality
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The user the object is currently lend to
        /// </summary>
        public virtual IUser LendTo { get; set; }
        /// <summary>
        /// The user that has reserved this object
        /// </summary>
        public virtual IUser ReservedBy { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor without any parameters for EntityFramework functionality
        /// </summary>
        public LearningUtility()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// When a user wants to reserve this object, you should call this method.
        /// This method checks if the user is of the correct type (Student). If not,
        /// the method throws an ArgumentException. Otherwise, the method calls the
        /// Reserve method of the current state.
        /// </summary>
        /// <param name="user">The user that tries to reserve the object</param>
        public void Reserve(IUser user)
        {
            if(user.GetType() != typeof(Student))
                throw new ArgumentException();
            currentState.Reserve(user);
        }

        /// <summary>
        /// When a user wants to block this object, you should call this method.
        /// This method checks if the user is of the correct type (Lector). If not,
        /// the method throws an ArgumentException. Otherwise, the method calls the
        /// Block method of the current state.
        /// </summary>
        /// <param name="user">The user that tries to block the object</param>
        public void Block(IUser user)
        {
            if(user.GetType() != typeof(Lector))
                throw new ArgumentException();
            currentState.Block(user);
        }

        /// <summary>
        /// When a user wants to cancel his reservation or block, you should call this method.
        /// This method calls the MakeAvailable method of the current state.
        /// </summary>
        public void MakeAvailable()
        {
            currentState.MakeAvailable();
        }

        /// <summary>
        /// You should call this method when the item is not returned in time.
        /// This method calls the Late method of the current state.
        /// </summary>
        public void Late()
        {
            currentState.Late();
        }

        /// <summary>
        /// This method changes the state of this object. This method should only be called
        /// from out of the children of the LearningUtilityState class or from the 
        /// LearningUtilityState class itself, and never from any other class.
        /// </summary>
        /// <param name="state">The state this object needs to change to</param>
        public void ToState(LearningUtilityState state)
        {
            this.currentState = state;
        }
        #endregion
    }
}