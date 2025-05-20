using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryMaster.Data;
using InventoryMaster.Models;

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
        public IActionResult GetEstoques()
        {
            var estoques = _context.Estoques.Include(e => e.Produto).ToList();
            return Ok(estoques);
        }

        // Atualiza a quantidade de um produto no estoque
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarEstoque(int id, [FromBody] int quantidade)
        {
            var estoque = await _context.Estoques.FindAsync(id);
            if (estoque == null) return NotFound();

            estoque.Quantidade = quantidade;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}