using System;
using System.Collections.Generic;

namespace InventoryMaster.Models
{
    // Modelo que representa uma venda
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public string Estado { get; set; } = "Em andamento"; // Exemplo de máquina de estados

        public List<ItemCarrinho> Itens { get; set; } = new();
    }
}