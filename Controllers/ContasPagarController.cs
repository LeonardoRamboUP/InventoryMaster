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
    public class ContasPagarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContasPagarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.ContasPagar.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var conta = _context.ContasPagar.Find(id);
            if (conta == null) return NotFound();
            return Ok(conta);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ContaPagar conta)
        {
            _context.ContasPagar.Add(conta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = conta.Id }, conta);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ContaPagar conta)
        {
            if (id != conta.Id) return BadRequest();
            _context.Entry(conta).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var conta = _context.ContasPagar.Find(id);
            if (conta == null) return NotFound();
            _context.ContasPagar.Remove(conta);
            _context.SaveChanges();
            return NoContent();
        }
    }
}