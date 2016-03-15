using DidactischeLeermiddelen.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.DAL.Mapper
{
    public class WishlistMapper : EntityTypeConfiguration<Wishlist>
    {
        public WishlistMapper()
        {
            HasMany(l => l.LearningUtilities).WithMany().Map(m =>
            {
                m.ToTable("Wishlist_LearningUtiltiy");
                m.MapLeftKey("WishlistId");
                m.MapRightKey("LearningUtilityId");
            });
        }
    }
}