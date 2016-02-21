using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class LocationMapper : EntityTypeConfiguration<Location>
    {

        public LocationMapper()
        {
            #region Properties
            Property(location => location.Name).IsRequired().HasMaxLength(100);

            #endregion
        }

    }
}