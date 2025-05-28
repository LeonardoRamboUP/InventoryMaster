using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InventoryMaster.Models
{
    // Item do carrinho de compras ou de uma venda
    public class ItemCarrinho
    {
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal PrecoUnitario { get; set; }

        public Produto? Produto { get; set; }
    }
}