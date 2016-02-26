using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class LearningUtilityDetailsMapper : EntityTypeConfiguration<LearningUtilityDetails>
    {
        public LearningUtilityDetailsMapper()
        {
            Property(l => l.Name).IsRequired().HasMaxLength(100);
            Property(l => l.Description).IsRequired().HasMaxLength(1000);
            Property(l => l.Picture).HasMaxLength(250);
        }
    }
}