using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain.Locations;

namespace DidactischeLeermiddelen.Models.DAL.Mappers
{
    public class LocationMapper : EntityTypeConfiguration<Location>
    {
        #region Constructor
        public LocationMapper()
        {
            ToTable("Location");
            HasRequired(location => location.City)
                                            .WithMany()
                                            .Map(m => m.MapKey("PostalCode"))
                                            .WillCascadeOnDelete(false);
    /*        HasRequired(location => location)
                                            .WithMany()
                                            .Map(m => m.MapKey("ClassroomId"))
                                            .WillCascadeOnDelete(false);
            Property(loc => loc.HouseNumber).HasMaxLength(5);
            */
        }
        #endregion
    }
}