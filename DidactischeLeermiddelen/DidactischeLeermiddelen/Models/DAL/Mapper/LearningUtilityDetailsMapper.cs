using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class LearningUtilityMapper : EntityTypeConfiguration<LearningUtility>
    {
        public LearningUtilityMapper()
        {
            Ignore(l => l.DateWanted);
            Property(l => l.Name).IsRequired().HasMaxLength(100);
            Property(l => l.Description).IsRequired().HasMaxLength(1000);
            Property(l => l.Picture).HasMaxLength(250);
            HasMany(u => u.Reservations).WithRequired(r => r.LearningUtility);


            HasMany(t => t.FieldsOfStudy).WithMany().Map(m =>
            {
                m.ToTable("LearningUtility_FieldOfStudy");
                m.MapLeftKey("LearningUtilityId");
                m.MapRightKey("FieldOfStudyId");
            });
            HasMany(t => t.TargetGroups).WithMany().Map(m =>
            {
                m.ToTable("LearningUtility_TargetGroup");
                m.MapLeftKey("LearningUtilityId");
                m.MapRightKey("TargetGroupId");
            });
        }
    }
}