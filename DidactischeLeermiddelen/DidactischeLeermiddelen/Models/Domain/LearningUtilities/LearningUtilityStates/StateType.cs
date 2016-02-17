﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates
{
    internal enum StateType
    {
        Available = 0,
        Unavailable = 1,
        Reserverd = 2,
        Blocked = 3,
        HandedOut = 4,
        Late = 5
    }
}