using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using InventoryMaster.Data;
using InventoryMaster.Models;

namespace InventoryMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os produtos
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        // Retorna um produto pelo ID
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            return product == null ? NotFound() : Ok(product);
        }

        // Cria um novo produto
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // Atualiza um produto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id) return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Remove um produto pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Busca produtos por nome
        [HttpGet("search")]
        public IActionResult SearchProducts([FromQuery] string name)
        {
            var products = _context.Products
                .Where(p => p.Name.Contains(name))
                .ToList();
            return Ok(products);
        }

        // Endpoint protegido por autenticação JWT
        [Authorize]
        [HttpGet("protegido")]
        public IActionResult GetProtected()
        {
            return Ok("Você está autenticado!");
        }

        // Retorna o total de produtos cadastrados
        [HttpGet("total")]
        public IActionResult GetTotalProducts()
        {
            var total = _context.Products.Count();
            return Ok(new { total });
        }
    }
}