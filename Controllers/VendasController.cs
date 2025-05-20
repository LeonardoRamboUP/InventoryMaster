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
    public class VendasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendasController(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas as vendas
        [HttpGet]
        public IActionResult GetVendas()
        {
            var vendas = _context.Vendas.Include(v => v.Itens).ToList();
            return Ok(vendas);
        }

        // Cria uma nova venda
        [HttpPost]
        public async Task<IActionResult> CreateVenda([FromBody] Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVenda), new { id = venda.Id }, venda);
        }

        // Retorna uma venda pelo ID
        [HttpGet("{id}")]
        public IActionResult GetVenda(int id)
        {
            var venda = _context.Vendas.Include(v => v.Itens).FirstOrDefault(v => v.Id == id);
            return venda == null ? NotFound() : Ok(venda);
        }

        // Atualiza o estado de uma venda
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> AtualizarEstado(int id, [FromBody] string novoEstado)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null) return NotFound();
            venda.Estado = novoEstado;
            await _context.SaveChangesAsync();
            return Ok(venda);
        }
    }
}