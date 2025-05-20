using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using InventoryMaster.Data;
using InventoryMaster.Models;

namespace InventoryMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os produtos
        [HttpGet]
        public IActionResult GetProdutos()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        // Retorna um produto pelo ID
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var produto = _context.Produtos.Find(id);
            return produto == null ? NotFound() : Ok(produto);
        }

        // Cria um novo produto
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar produto: {ex.Message}");
            }
        }

        // Atualiza um produto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id) return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Remove um produto pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Busca produtos por nome
        [HttpGet("search")]
        public IActionResult SearchProducts([FromQuery] string nome)
        {
            var produtos = _context.Produtos
                .Where(p => p.Nome.Contains(nome))
                .ToList();
            return Ok(produtos);
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
            var total = _context.Produtos.Count();
            return Ok(new { total });
        }
    }
}