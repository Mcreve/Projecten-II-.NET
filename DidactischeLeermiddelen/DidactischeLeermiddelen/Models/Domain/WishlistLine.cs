using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;

namespace DidactischeLeermiddelen.Models.Domain
{
    public class WishlistLine
    {
        public LearningUtilityDetails LearningUtility { get; set; }
        public int Quantity { get; set; }
    }
}