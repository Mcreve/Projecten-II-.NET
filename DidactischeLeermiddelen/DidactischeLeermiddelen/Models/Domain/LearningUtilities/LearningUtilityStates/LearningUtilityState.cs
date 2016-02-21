using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    /// <summary>
    /// The base class for the different states of a LearningUtility.
    /// This class implements the default behavior for the methods.
    /// Each method that doesn't need to throw an InvalidOperationException, 
    /// should be overridden in the subclass.
    /// Methods that don't need any specific implementation other than throwing an 
    /// InvalidOperationException shouldn't be overridden in the subclass.
    /// </summary>
    public class LearningUtilityState
    {
        #region Properties
        protected virtual LearningUtility LearningUtility { get; set; } 
        #endregion

        #region Constructors
        /// <summary>
        /// This consturctor needs to be used by the concrete implementations of the states. 
        /// This constructor sets the LearningUtility instance that is owner of the state.
        /// </summary>
        /// <param name="learningUtility">The LearningUtility instance that is owner of the state</param>
        public LearningUtilityState(LearningUtility learningUtility)
        {
            this.LearningUtility = learningUtility;
        }

        /// <summary>
        /// No parameter constructor for EntityFramework functionality
        /// </summary>
        public LearningUtilityState() { }
        #endregion

        #region Methods
        /// <summary>
        /// Default implementation of this method throws an InvalidOperationException.
        /// </summary>
        /// <param name="user">A user instance that will reserve the LearningUtility</param>
        public virtual void Reserve(IUser user)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Default implemention of this method throws an InvalidOperationException.
        /// </summary>
        /// <param name="user">A user instance that will block the LearningUtility</param>
        public virtual void Block(IUser user)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Default implementation of this method throws an InvalidOperationException.
        /// </summary>
        public virtual void Late()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Default implementation of this method throws an InvalidOperationException.
        /// </summary>
        public virtual void MakeAvailable()
        {
            throw new InvalidOperationException();
        } 
        #endregion

    }
}