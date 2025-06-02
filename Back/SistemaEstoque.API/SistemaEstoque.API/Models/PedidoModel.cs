using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.DTOs.CadastrosDTOs;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.Models
{
    public class PedidoModel
    {
        public int id { get; set; }
        public int qtd { get; set; }
        public int ClienteId { get; set; }
        public ClienteModel Cliente { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public List<PedidoXProduto> Produto { get; set; }
        public short Finalizado { get; set; }
        public DateTime dthPedido { get; set; }

        public static implicit operator PedidoModel(PedidoDTO dto)
        {
            return new PedidoModel
            {
                id = dto.id,
                qtd = dto.qtd,
                ClienteId = dto.ClienteId,
                UsuarioId = dto.UsuarioId,
                Finalizado = dto.Finalizado,
                dthPedido = dto.dthPedido,
            };
        }

    }
}
