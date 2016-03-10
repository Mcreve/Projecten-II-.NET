using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class CompanyMapper : EntityTypeConfiguration<Company>
    {

        public CompanyMapper()
        {
            #region Properties
            Property(company => company.Name).IsRequired().HasMaxLength(100);
            Property(company => company.EmailAddress).HasMaxLength(100);
            Property(company => company.ContactPersonName).HasMaxLength(100);
            Property(company => company.Website).HasMaxLength(100);
            #endregion
        }

    }
}