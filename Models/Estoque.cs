using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InventoryMaster.Models
{
    // Modelo que representa o estoque de um produto
    public class Estoque
    {
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public Produto? Produto { get; set; }
    }
}