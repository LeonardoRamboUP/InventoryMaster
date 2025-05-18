namespace InventoryMaster.Models
{
    // Modelo que representa um funcionário no sistema
    public class Funcionario
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}