namespace InventoryMaster.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using InventoryMaster.Data;
    using InventoryMaster.Models;

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
        public IActionResult GetFuncionarios()
        {
            var funcionarios = _context.Funcionarios.ToList();
            return Ok(funcionarios);
        }

        // Retorna um funcionário pelo ID
        [HttpGet("{id}")]
        public IActionResult GetFuncionario(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            return funcionario == null ? NotFound() : Ok(funcionario);
        }

        // Cria um novo funcionário
        [HttpPost]
        public async Task<IActionResult> CreateFuncionario([FromBody] Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
        }

        // Atualiza um funcionário existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuncionario(int id, [FromBody] Funcionario funcionario)
        {
            if (id != funcionario.Id) return BadRequest();

            _context.Entry(funcionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Remove um funcionário pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound();

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}