using System.Collections.Generic;

namespace DidactischeLeermiddelen.Models.Domain.Locations
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Classroom> Classroom { get; set; }

    }
}