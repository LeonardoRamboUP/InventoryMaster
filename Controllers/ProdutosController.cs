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
        public IActionResult ObterTodos()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        // Retorna um produto pelo ID
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var produto = _context.Produtos.Find(id);
            return produto == null ? NotFound("Produto não encontrado.") : Ok(produto);
        }

        // Cria um novo produto
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar produto: {ex.Message}");
            }
        }

        // Atualiza um produto existente
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id) return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Remove um produto pelo ID
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound("Produto não encontrado.");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}