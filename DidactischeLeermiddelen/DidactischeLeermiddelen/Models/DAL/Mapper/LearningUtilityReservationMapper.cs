using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class ReservationMapper : EntityTypeConfiguration<Reservation>
    {
        public ReservationMapper()
        {
            HasKey(res => res.Id);
            HasRequired(res => res.User).WithMany(u => u.Reservations).HasForeignKey(res => res.UserEmail);
            HasRequired(res => res.LearningUtility).WithMany(u => u.Reservations).HasForeignKey(res => res.LearningUtilityId);
        }
    }
}