using System.ComponentModel.DataAnnotations;

namespace Demo.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string Amenity{ get; set; }
    }
}
