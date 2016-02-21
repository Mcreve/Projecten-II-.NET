using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class TargetGroupMapper : EntityTypeConfiguration<TargetGroup>
    {
        public TargetGroupMapper()
        {
            #region Properties
            Property(targetGroup => targetGroup.Name).IsRequired().HasMaxLength(100);
            #endregion
        }

    }
}