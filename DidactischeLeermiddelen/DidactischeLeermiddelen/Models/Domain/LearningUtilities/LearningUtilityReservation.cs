﻿using DidactischeLeermiddelen.Models.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.LearningUtilities
{
    public class LearningUtilityReservation
    {
        public int Week { get; set; }
        public User User { get; set; }
        public int Amount { get; set; }
    }
}