using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMaster.Models
{
    // Modelo que representa um carrinho de compras
    public class Carrinho
    {
        public int Id { get; set; }
        public List<ItemCarrinho> Itens { get; set; } = new();
    }
}