using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class LearningUtility
    {
        #region Fields

        private LearningUtilityState currentState;
        #endregion

        #region Properties
        public virtual LearningUtilityState CurrentState { get { return currentState; } }
        public int Id { get; set; }
        public virtual IUser LendTo { get; set; }
        public virtual IUser ReservedBy { get; set; }
        #endregion

        #region Constructors

        public LearningUtility()
        {
        }
        #endregion

        #region Methods
        public void Reserve(IUser user)
        {
            currentState.Reserve(user);
        }

        public void Block(IUser user)
        {
            currentState.Block(user);
        }

        public void MakeAvailable()
        {
            currentState.MakeAvailable();
        }

        public void Late()
        {
            currentState.Late();
        }

        public void ToState(LearningUtilityState state)
        {
            this.currentState = state;
        }
        #endregion
    }
}