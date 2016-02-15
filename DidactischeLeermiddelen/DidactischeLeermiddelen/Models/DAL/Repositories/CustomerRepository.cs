using System.Data.Entity;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Interfaces;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Models.DAL.Repositories
{
    /// <summary>
    /// Communication with the DbSet Customerss
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        #region Properties
        private readonly LeermiddelenContext context;
        private readonly DbSet<Customer> customers;
        #endregion

        #region Constructor
        public CustomerRepository(LeermiddelenContext context)
        {
            this.context = context;
            customers = context.Customers;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the customer with the specific emailadres
        /// </summary>
        /// <param name="emailAdres"></param>
        /// <returns>Customer with that e-mail adres</returns>
        public Customer FindByEmail(string emailAdres)
        {
            return customers.FirstOrDefault(customer => customer.Email == emailAdres);
        }

        public Customer FindById(string id)
        {
            return customers.Find(id);
        }


        public IQueryable<Customer> FindAll()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Saves changes to the context
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Adds a customer to the repository, don't forget to save!
        /// Aan te passen?
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>void</returns>
        public void Add(Customer customer)
        {
            customers.Add(customer);
        } 
        #endregion
    }
}