using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain.Locations;

namespace DidactischeLeermiddelen.Models.DAL.Mappers
{
    public class CityMapper : EntityTypeConfiguration<City>
    {
        #region Constructor
        public CityMapper()
        {
            ToTable("City");
            HasKey(city => city.PostalCode);

            Property(city => city.PostalCode).IsFixedLength()
                                             .HasColumnType("char")
                                             .HasMaxLength(4);
            Property(city => city.Name).IsRequired()
                                       .HasMaxLength(100);
        }
        #endregion
    }
}