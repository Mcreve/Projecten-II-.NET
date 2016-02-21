using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class FieldOfStudyMapper : EntityTypeConfiguration<FieldOfStudy>
    {

        public FieldOfStudyMapper()
        {
            #region Properties
            Property(fieldOfStudy => fieldOfStudy.Name).IsRequired().HasMaxLength(100);

            #endregion
        }

    }
}