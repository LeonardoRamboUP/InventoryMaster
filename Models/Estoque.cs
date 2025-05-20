using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMaster.Models
{
    // Modelo que representa o estoque de um produto
    public class Estoque
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantidade { get; set; }

        public Produto? Produto { get; set; }
    }
}