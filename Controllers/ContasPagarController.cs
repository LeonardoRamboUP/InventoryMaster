using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryMaster.Data;
using InventoryMaster.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace InventoryMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasPagarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContasPagarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ObterTodas()
        {
            return Ok(_context.ContasPagar.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var conta = _context.ContasPagar.Find(id);
            if (conta == null) return NotFound("Conta a pagar não encontrada.");
            return Ok(conta);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar([FromBody] ContaPagar conta)
        {
            _context.ContasPagar.Add(conta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = conta.Id }, conta);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ContaPagar conta)
        {
            if (id != conta.Id) return BadRequest();
            _context.Entry(conta).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var conta = _context.ContasPagar.Find(id);
            if (conta == null) return NotFound("Conta a pagar não encontrada.");
            _context.ContasPagar.Remove(conta);
            _context.SaveChanges();
            return NoContent();
        }
    }
}