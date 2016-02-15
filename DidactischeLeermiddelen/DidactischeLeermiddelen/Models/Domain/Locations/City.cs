using System;

namespace DidactischeLeermiddelen.Models.Domain.Locations
{
    public class City
    {
        private string name;
        private string postalCode;

        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                CheckNullOrWhiteSpace(nameof(PostalCode), value);
                CheckMaxLength(nameof(PostalCode), value);
                postalCode = value.ToLower();
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                CheckNullOrWhiteSpace(nameof(Name), value);
                CheckMaxLength(nameof(Name), value);
                name = value.ToLower();
            }
        }

        public City()
        {
        }
        public City(string name, string postalCode)
        {
            this.Name = name;
            this.PostalCode = postalCode;
        }



        #region Private Methods
        private static void CheckNullOrWhiteSpace(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(propertyName + " moet ingevuld zijn.");
            }
        }
        private void CheckMaxLength(string propertyName, string value)
        {
            var maxLength = propertyName == "PostalCode" ? 4 : 100;
            if (value.Length > maxLength)
            {
                throw new ArgumentException(propertyName + " mag maximaal " + maxLength + " karakters lang zijn.");
            }
        } 
        #endregion
    }
}