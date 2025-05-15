using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.Models
{
    public class TipoProdutoModel
    {
        public int id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public short fAtivo { get; set; }
    }
}