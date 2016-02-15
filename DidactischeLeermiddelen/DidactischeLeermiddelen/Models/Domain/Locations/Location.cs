using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DidactischeLeermiddelen.Models.Domain.Locations
{
    public class Location
    {
        private string name;
        private string street;
        private string houseNumber;
        private City city;

        public int LocationId { get; set; }
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
        public string Street
        {
            get { return street; }
            set
            {
                CheckNullOrWhiteSpace(nameof(Street), value);
                CheckMaxLength(nameof(Street), value);
                street = value.ToLower();
            }
        }
        public string HouseNumber
        {
            get { return houseNumber; }
            set
            {
                CheckNullOrWhiteSpace(nameof(HouseNumber), value);
                CheckMaxLength(nameof(HouseNumber), value);
                houseNumber = value.ToLower();
            }
        }

        public virtual City City
        {
            get { return city; }
            set
            {
                if(value == null)
                { throw new ArgumentNullException("City is null");}
                city = value;
;            }
        }

        public virtual ICollection<Classroom> Classrooms { get; set; }

        public Location()
        {
            Classrooms = new List<Classroom>();
        }

        public Location(string name, string street, string housenumber, City city):this()
        {
            this.Name = name;
            this.Street = street;
            this.HouseNumber = housenumber;
            this.City = city;
        }
        #region Public Methods

        public void AddClassroom(Classroom classroom)
        {
            if (classroom == null)
                throw new ArgumentNullException("Classroom is null");

                Classrooms.Add(classroom);

        }
        public void RemoveClassRoom(Classroom classroom)
        {
            if (classroom == null)
                throw new ArgumentNullException("Classroom is null");

           Classrooms.Remove(classroom);
            
        }
        public Classroom FindClassRoomByName(string name)
        {
            return Classrooms.FirstOrDefault(classroom => classroom.Name == name);
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
            var maxLength = propertyName == "HouseNumber" ? 5 : 100;
            if (value.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(propertyName + " mag maximaal " + maxLength + " karakters lang zijn.");
            }
        }
#endregion
    }
}