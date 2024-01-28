namespace ApiRest.Controllers.dto
{
    public class ProductDto
    {
        public required int LocalId { get; set; }
        public required string Name { get; set; }

        public required decimal Price { get; set; }
    }
}
