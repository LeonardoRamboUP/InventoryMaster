// Exemplo para Product (Produto.cs)
using System.ComponentModel.DataAnnotations;

namespace InventoryMaster.Models
{
    // Modelo que representa um produto no sistema
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; } = string.Empty; 

        [Range(0, int.MaxValue)]
        public int Quantidade { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Preco { get; set; }
    }
}