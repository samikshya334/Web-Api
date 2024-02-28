using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Villa
    {
      
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string Amenity { get; set; }
    }
}

