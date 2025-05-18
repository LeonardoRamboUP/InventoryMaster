using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMaster.Models
{
    // Item do carrinho de compras ou de uma venda
    public class ItemCarrinho
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public Product? Product { get; set; }
    }
}