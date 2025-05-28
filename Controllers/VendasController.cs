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
    public class VendasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendasController(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas as vendas
        [HttpGet]
        public IActionResult ObterTodas()
        {
            var vendas = _context.Vendas
                .Include(v => v.Itens)
                .Include(v => v.Funcionario)
                .ToList();
            return Ok(vendas);
        }

        // Cria uma nova venda
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Venda venda)
        {
            var funcionario = await _context.Funcionarios.FindAsync(venda.FuncionarioId);
            if (funcionario == null)
                return BadRequest("Funcionário não encontrado.");

            venda.Total = venda.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
        }

        // Retorna uma venda pelo ID
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var venda = _context.Vendas.Include(v => v.Itens).FirstOrDefault(v => v.Id == id);
            return venda == null ? NotFound("Venda não encontrada.") : Ok(venda);
        }

        // Atualiza o estado de uma venda
        [Authorize]
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> AtualizarEstado(int id, [FromBody] string novoEstado)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null) return NotFound("Venda não encontrada.");
            venda.Estado = novoEstado;
            await _context.SaveChangesAsync();
            return Ok(venda);
        }
    }
}