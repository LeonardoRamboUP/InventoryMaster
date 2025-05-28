using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryMaster.Data;
using InventoryMaster.Models;

namespace InventoryMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os funcionários
        [HttpGet]
        public IActionResult ObterTodos()
        {
            var funcionarios = _context.Funcionarios.ToList();
            return Ok(funcionarios);
        }

        // Retorna um funcionário pelo ID
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            return funcionario == null ? NotFound("Funcionário não encontrado.") : Ok(funcionario);
        }

        // Cria um novo funcionário
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = funcionario.Id }, funcionario);
        }

        // Atualiza um funcionário existente
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Funcionario funcionario)
        {
            if (id != funcionario.Id) return BadRequest();

            _context.Entry(funcionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Remove um funcionário pelo ID
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound("Funcionário não encontrado.");

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}