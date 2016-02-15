using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class Customer
    {
        #region Fields

        private string name;
        private string firstName;
        private string email;
#endregion

        #region Properties
        public int CustomerId { get; set; }
        [Display(Name = "E-mail")]
        public string Email
        {
            get { return email; }
            set
            {
                CheckNullOrWhiteSpace(nameof(Email), value);
                CheckMaxLength(nameof(Email), value);
                CheckHoGentMail(value);
                email = value.ToLower();
            }
        }
        [Display(Name = "Naam")]

        public string Name
        { get { return name; }
            set
            {
                CheckNullOrWhiteSpace(nameof(Name), value);
                CheckMaxLength(nameof(Name), value);
                name = value.ToLower();
            }
        }
        [Display(Name = "Voornaam")]

        public string FirstName {
            get { return firstName; }
            set
            {
                CheckNullOrWhiteSpace(nameof(FirstName),value);
                CheckMaxLength(nameof(FirstName),value);
                firstName = value.ToLower();
            }
        }
        #endregion

#region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <returns>A new customer object</returns>
        public Customer()
        {
            //Later development goes here, Cart, reservations...
        }
        /// <summary>
        /// Constructor with some parameters, calls the default constructor
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="firstName"></param>
        /// <returns>a new Customer object</returns>
        public Customer(string name, string firstName, string email) :this()
        {
            this.Email = email.ToLower();
            this.Name = name.ToLower();
            this.FirstName = firstName.ToLower();
        }
        #endregion


#region private methods
        private static void CheckNullOrWhiteSpace(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(propertyName + " moet ingevuld zijn.");
            }
        }
        private void CheckMaxLength(string propertyName, string value)
        {
            if (value.Length > 100)
            {
                throw new ArgumentException(propertyName + " mag maximaal 100 karakters lang zijn.");
            }
        }

        private static void CheckHoGentMail(string value)
        {

            Regex regex = new Regex(@"(?i)hogent\.be$");
            Match match = regex.Match(value);
            if (!match.Success)
               throw new ArgumentException("Email : " + value + "is geen geldig HoGent E-mail Adres");
        }
        #endregion
    }


}