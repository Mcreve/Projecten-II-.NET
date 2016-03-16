using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class UserMapper : EntityTypeConfiguration<User>
    {

        public UserMapper()
        {
            #region Keys
            HasKey(user => user.EmailAddress);
            HasMany(l => l.Reservations).WithMany().Map(m =>
            {
                m.ToTable("User_Reservation");
                m.MapLeftKey("UserId");
                m.MapRightKey("ReservationId");
            });
            
            #endregion

            #region Properties
            Property(user => user.EmailAddress).IsRequired().HasMaxLength(100);
            Property(user => user.FirstName).IsRequired().HasMaxLength(100);
            Property(user => user.LastName).IsRequired().HasMaxLength(100);
            #endregion
        }

    }
}