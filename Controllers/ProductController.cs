using ApiRest.Context;
using ApiRest.Controllers.dto;
using ApiRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProductById(int id) {
            if (_context == null) {
                return NotFound("Contexto productos no encontrado");
            }
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Producto no encontrado");
            }
            return product;    
        }

        [HttpPost]
        public async Task<ActionResult<Product>> saveProduct(ProductDto productDto) {
            if (_context.products == null) {
                return NotFound();
            }
            try {
                var local = await _context.locals.FindAsync(productDto.LocalId);
                
                if (local == null) {
                    return NotFound("Local no encontrado");
                }
                var product = new Product() {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    LocalId = productDto.LocalId,
                    local = local
                };
                _context.products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction("getProductById", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
