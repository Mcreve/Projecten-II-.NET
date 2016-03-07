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
            Ignore(l => l.DateWanted);
            Property(l => l.Name).IsRequired().HasMaxLength(100);
            Property(l => l.Description).IsRequired().HasMaxLength(1000);
            Property(l => l.Picture).HasMaxLength(250);
            Property(l => l.TimeStamp).HasColumnType("timestamp");
            HasMany(t => t.FieldsOfStudy).WithMany().Map(m =>
            {
                m.ToTable("LearningUtilityDetails_FieldOfStudy");
                m.MapLeftKey("LearningUtilityId");
                m.MapRightKey("FieldOfStudyId");
            });
            HasMany(t => t.TargetGroups).WithMany().Map(m =>
            {
                m.ToTable("LearningUtilityDetails_TargetGroup");
                m.MapLeftKey("LearningUtilityId");
                m.MapRightKey("TargetGroupId");
            });
        }
    }
}