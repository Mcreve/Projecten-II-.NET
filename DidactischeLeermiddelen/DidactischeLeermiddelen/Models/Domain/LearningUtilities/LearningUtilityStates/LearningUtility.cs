using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public class LearningUtility
    {
        #region Fields
        private LearningUtilityState currentState;
        #endregion

        #region Properties
        public IUser LendTo { get; set; }
        #endregion

        #region Constructors
        public LearningUtility() { }
        #endregion

        #region Methods
        public void Reserve()
        {
            currentState.Reserve();
        }

        public void Block()
        {
            currentState.Block();
        }

        public void HandOut()
        {
            currentState.HandOut();
        }

        public void MakeAvailable()
        {
            currentState.MakeAvailable();
        }

        public void MakeUnavailable()
        {
            currentState.MakeUnavailable();
        }

        private void Late()
        {
            currentState.Late();
        }

        internal void ToState(LearningUtilityState state)
        {
            this.currentState = state;
        }
        #endregion
    }
}