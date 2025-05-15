using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.Models
{
    public class CidadeModel
    {
        public int id { get; set; }
        public string Cidade { get; set; }
        public EstadoModel Estado { get; set; }

    }
}
