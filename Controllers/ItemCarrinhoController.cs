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
    public class ItemCarrinhoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemCarrinhoController(AppDbContext context)
        {
            _context = context;
        }

        // Lista todos os itens do carrinho
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var itens = await _context.ItensCarrinho
                .Include(i => i.Produto)
                .ToListAsync();
            return Ok(itens);
        }

        // Busca um item do carrinho por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.ItensCarrinho
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return NotFound();
            return Ok(item);
        }

        // Adiciona um novo item ao carrinho
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemCarrinho item)
        {
            _context.ItensCarrinho.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        // Atualiza um item do carrinho
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemCarrinho item)
        {
            if (id != item.Id) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Remove um item do carrinho
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ItensCarrinho.FindAsync(id);
            if (item == null) return NotFound();

            _context.ItensCarrinho.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}