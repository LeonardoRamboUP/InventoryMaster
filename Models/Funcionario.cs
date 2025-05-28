using System.ComponentModel.DataAnnotations;

namespace InventoryMaster.Models
{
    // Modelo que representa um funcionário no sistema
    public class Funcionario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Cargo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Salario { get; set; }
    }
}