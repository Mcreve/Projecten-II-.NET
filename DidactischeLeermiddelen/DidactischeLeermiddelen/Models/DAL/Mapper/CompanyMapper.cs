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
            #endregion
        }

    }
}