namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    public enum StateType
    {
        NotInitialized = 0,
        Available = 1,
        Unavailable = 2,
        Reserved = 3,
        Blocked = 4,
        HandedOut = 5,
        Late = 6
    }
}