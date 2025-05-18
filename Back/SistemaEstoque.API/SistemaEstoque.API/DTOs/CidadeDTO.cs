using SistemaEstoque.API.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.DTOs
{
    public class CidadeDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Cidade e obrigatorio")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Estado e obrigatorio")]
        public EstadoDTO Estado { get; set; }

        public static implicit operator CidadeDTO(CidadeModel cidade)
        {
            return new CidadeDTO
            {
                Cidade = cidade.Cidade,
                id = cidade.id,
                Estado = cidade.Estado
            };
        }

    }
}
