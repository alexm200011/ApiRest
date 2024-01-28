using System.ComponentModel.DataAnnotations;

namespace ApiRest.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 

        public required string Name { get; set; }

        public required decimal Price { get; set; }

        public int LocalId { get; set; }
        public Local local { get; set; }

    }
}
