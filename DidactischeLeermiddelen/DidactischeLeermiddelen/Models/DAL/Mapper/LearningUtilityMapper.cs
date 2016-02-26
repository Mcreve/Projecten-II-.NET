using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class LearningUtilityMapper : EntityTypeConfiguration<LearningUtility>
    {
        public LearningUtilityMapper()
        {
            Property(l => l.Timestamp).HasColumnType("timestamp");
        }
    }
}