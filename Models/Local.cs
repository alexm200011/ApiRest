using System.ComponentModel.DataAnnotations;

namespace ApiRest.Models
{
    public class Local
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Floor { get; set; }
        public required string Code { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
