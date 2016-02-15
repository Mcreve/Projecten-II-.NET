using System.Data.Entity.ModelConfiguration;
using DidactischeLeermiddelen.Models.Domain;

namespace DidactischeLeermiddelen.Models.DAL.Mappers
{
    /// <summary>
    /// Maps the Customer Class to the Table Customer
    /// </summary>
    public class CustomerMapper : EntityTypeConfiguration<Customer>
    {
        #region Constructor
        public CustomerMapper()
        {
            Property(c => c.Name).IsRequired().HasMaxLength(100);
            Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            Property(c => c.Email).IsRequired().HasMaxLength(100);
            ToTable("Customer");
        } 
        #endregion
    }
}