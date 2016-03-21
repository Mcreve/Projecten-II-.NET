using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class LeermiddelenContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor

        public LeermiddelenContext() : base("Leermiddelen")
        {
        }

        #endregion

        #region Properties

        public DbSet<User> UserList { get; set; }
        public DbSet<LearningUtility> LearningUtilities { get; set; }
        public DbSet<TargetGroup> TargetGroups { get; set; }
        public DbSet<FieldOfStudy> FieldsOfStudies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        #endregion

        #region Methods

        /// <summary>
        ///     When the model is created, give specifications
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());


        }

        /// <summary>
        ///     Creates the context of the application
        /// </summary>
        /// <returns></returns>
        public static LeermiddelenContext Create()
        {
            return DependencyResolver.Current.GetService<LeermiddelenContext>();
        }
   
        #endregion
    }
}