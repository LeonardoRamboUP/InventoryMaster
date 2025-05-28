using Microsoft.AspNetCore.Mvc;
using InventoryMaster.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace InventoryMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelatoriosController(AppDbContext context)
        {
            _context = context;
        }

        // Relatório financeiro: totais a pagar e a receber
        [Authorize]
        [HttpGet("financeiro")]
        public IActionResult Financeiro()
        {
            var totalReceber = _context.ContasReceber.Where(c => !c.Recebido).Sum(c => c.Valor);
            var totalPagar = _context.ContasPagar.Where(c => !c.Pago).Sum(c => c.Valor);
            return Ok(new { totalReceber, totalPagar });
        }

        // Relatório de estoque: lista de produtos e quantidades
        [Authorize]
        [HttpGet("estoque")]
        public IActionResult Estoque()
        {
            var estoque = _context.Estoques
                .Include(e => e.Produto)
                .Select(e => new
                {
                    Produto = e.Produto.Nome,
                    e.Quantidade
                })
                .ToList();
            return Ok(estoque);
        }

        // Relatório de vendas: lista de vendas e valores totais
        [Authorize]
        [HttpGet("vendas")]
        public IActionResult Vendas()
        {
            var vendas = _context.Vendas
                .Include(v => v.Itens)
                .Select(v => new
                {
                    v.Id,
                    v.Data,
                    v.Estado,
                    Total = v.Itens.Sum(i => i.PrecoUnitario * i.Quantidade)
                })
                .ToList();
            return Ok(vendas);
        }
    }
}