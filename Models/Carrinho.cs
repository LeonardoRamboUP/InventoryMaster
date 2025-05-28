using System.Collections.Generic;

namespace InventoryMaster.Models
{
    // Modelo que representa um carrinho de compras
    public class Carrinho
    {
        public int Id { get; set; }
        public List<ItemCarrinho> Itens { get; set; } = new();
    }
}