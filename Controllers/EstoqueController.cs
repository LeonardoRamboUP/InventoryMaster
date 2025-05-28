using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryMaster.Data;
using InventoryMaster.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoryMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstoqueController(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os registros de estoque
        [HttpGet]
        public IActionResult ObterTodos()
        {
            var estoques = _context.Estoques.Include(e => e.Produto).ToList();
            return Ok(estoques);
        }

        // Retorna um registro de estoque por produto
        [HttpGet("produto/{produtoId}")]
        public IActionResult ObterPorProduto(int produtoId)
        {
            var estoque = _context.Estoques.Include(e => e.Produto).FirstOrDefault(e => e.ProdutoId == produtoId);
            if (estoque == null) return NotFound("Estoque não encontrado.");
            return Ok(estoque);
        }

        // Atualiza a quantidade de um produto no estoque
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] int quantidade)
        {
            var estoque = await _context.Estoques.FindAsync(id);
            if (estoque == null) return NotFound("Estoque não encontrado.");

            estoque.Quantidade = quantidade;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Adiciona um novo registro de estoque
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Estoque estoque)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Verifica se o produto existe
            var produto = await _context.Produtos.FindAsync(estoque.ProdutoId);
            if (produto == null)
                return BadRequest("Produto não encontrado.");

            // Verifica se já existe estoque para esse produto
            var estoqueExistente = await _context.Estoques.FirstOrDefaultAsync(e => e.ProdutoId == estoque.ProdutoId);
            if (estoqueExistente != null)
                return BadRequest("Já existe um registro de estoque para este produto.");

            _context.Estoques.Add(estoque);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterTodos), new { id = estoque.Id }, estoque);
        }

        // Remove um registro de estoque
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var estoque = await _context.Estoques.FindAsync(id);
            if (estoque == null) return NotFound("Estoque não encontrado.");

            _context.Estoques.Remove(estoque);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}