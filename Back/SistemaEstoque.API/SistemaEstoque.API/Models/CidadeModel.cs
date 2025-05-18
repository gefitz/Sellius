using SistemaEstoque.API.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.Models
{
    public class CidadeModel
    {
        public int id { get; set; }
        public string Cidade { get; set; }
        public EstadoModel Estado { get; set; }

        public static implicit operator CidadeModel(CidadeDTO cidade)
        {
            return new CidadeModel
            {
                Cidade = cidade.Cidade,
                id = cidade.id,
                Estado = cidade.Estado
            };
        }

    }
}
