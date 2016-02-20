using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class LeermiddelenContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<User> UserList { get; set; }

        public LeermiddelenContext():base("Leermiddelen")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static LeermiddelenContext Create()
        {
            return DependencyResolver.Current.GetService<LeermiddelenContext>();
        }
    }
}