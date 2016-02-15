using System;

namespace DidactischeLeermiddelen.Models.Domain.Locations
{
    public class Classroom
    {
        private string name;
        public int ClassroomId { get; set; }
        public string Name {
            get { return name; }
            set
            {
                CheckNullOrWhiteSpace(nameof(Name), value);
                CheckMaxLength(nameof(Name), value);
                name = value.ToLower();
            }
        }

        public Classroom()
        {
            
        }
        public Classroom(string name)
        {
            this.Name = name;
        }
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
                throw new ArgumentOutOfRangeException(propertyName + " mag maximaal 100 karakters lang zijn.");
            }
        }
        #endregion
    }
}