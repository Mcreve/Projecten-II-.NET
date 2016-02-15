using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain.Locations;

namespace DidactischeLeermiddelen.Models.DAL.Mappers
{
    public class ClassroomMapper : EntityTypeConfiguration<Classroom>
    {
        #region Constructor
        public ClassroomMapper()
        {
            ToTable("Classroom");
            HasKey(classroom => classroom.ClassroomId);
            Property(classroom => classroom.Name).IsRequired()
                                                 .HasMaxLength(100);
        }
        #endregion
    }
}