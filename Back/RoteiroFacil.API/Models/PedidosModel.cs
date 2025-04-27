namespace RoteiroFacil.API.Models
{
    public class PedidosModel
    {
        public int id { get; set; }
        public int ClienteId { get; set; }
        public ClienteModel Cliente { get; set; }
        public DateTime DthPedido { get; set; }

        public List<PedidoProdutoModel> PedidoProdutos { get; set; }
    }
}