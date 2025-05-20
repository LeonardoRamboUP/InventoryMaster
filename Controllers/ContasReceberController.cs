using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryMaster.Data;
using InventoryMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasReceberController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContasReceberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.ContasReceber.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var conta = _context.ContasReceber.Find(id);
            if (conta == null) return NotFound();
            return Ok(conta);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ContaReceber conta)
        {
            _context.ContasReceber.Add(conta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = conta.Id }, conta);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ContaReceber conta)
        {
            if (id != conta.Id) return BadRequest();
            _context.Entry(conta).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var conta = _context.ContasReceber.Find(id);
            if (conta == null) return NotFound();
            _context.ContasReceber.Remove(conta);
            _context.SaveChanges();
            return NoContent();
        }
    }
}