namespace RoteiroFacil.API.Models
{
    public class PedidoProdutoModel
    {
        public int id { get; set; }

        public int PedidoId {  get; set; }
        public PedidosModel Pedido { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }
        public int qtd { get; set; }
        public float ValorVenda { get; set; }
    }
}
