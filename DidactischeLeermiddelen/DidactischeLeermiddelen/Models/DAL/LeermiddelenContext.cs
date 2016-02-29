using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public DbSet<LearningUtilityDetails> LearningUtilityDetailsList { get; set; }
        public DbSet<TargetGroup> TargetGroups { get; set; }
        public DbSet<FieldOfStudy> FieldsOfStudy { get; set; }

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
            modelBuilder.Ignore<LearningUtilityState>();
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