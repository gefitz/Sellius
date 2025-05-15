using SistemaEstoque.API.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.Models
{
    public class PedidoModel
    {
        public int id { get; set; }
        public int qtd { get; set; }
        public ClienteModel Cliente { get; set; }
        public UsuarioModel Usuario { get; set; }
        public IEnumerable<PedidoXProduto> Produto { get; set; }
        public short Finalizado { get; set; }
        public DateTime dthPedido { get; set; }

    }
}
