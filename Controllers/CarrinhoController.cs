using Microsoft.AspNetCore.Mvc;
using InventoryMaster.Data;
using InventoryMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarrinhoController(AppDbContext context)
        {
            _context = context;
        }

        // Adiciona um item ao carrinho
        [HttpPost("adicionar-item")]
        public async Task<IActionResult> AdicionarItem([FromBody] ItemCarrinho item)
        {
            _context.ItensCarrinho.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // Remove um item do carrinho
        [HttpDelete("remover-item/{itemId}")]
        public async Task<IActionResult> RemoverItem(int itemId)
        {
            var item = await _context.ItensCarrinho.FindAsync(itemId);
            if (item == null) return NotFound();
            _context.ItensCarrinho.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Finaliza o carrinho (gera uma venda)
        [HttpPost("finalizar/{carrinhoId}")]
        public async Task<IActionResult> FinalizarCarrinho(int carrinhoId)
        {
            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.Id == carrinhoId);

            if (carrinho == null) return NotFound();

            // Aqui você pode criar uma venda a partir do carrinho
            var venda = new Venda
            {
                Data = System.DateTime.Now,
                Itens = carrinho.Itens.ToList(),
                Estado = "Em andamento"
            };
            _context.Vendas.Add(venda);

            // Opcional: Limpar ou marcar o carrinho como finalizado
            _context.Carrinhos.Remove(carrinho);

            await _context.SaveChangesAsync();
            return Ok(venda);
        }

        // Lista os itens de um carrinho
        [HttpGet("{carrinhoId}/itens")]
        public IActionResult ListarItens(int carrinhoId)
        {
            var carrinho = _context.Carrinhos
                .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefault(c => c.Id == carrinhoId);

            if (carrinho == null) return NotFound();
            return Ok(carrinho.Itens);
        }
    }
}