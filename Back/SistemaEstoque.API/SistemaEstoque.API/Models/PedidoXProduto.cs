namespace SistemaEstoque.API.Models
{
    public class PedidoXProduto
    {
        public int id { get; set; }

        public int idPedido { get; set; }
        public PedidoModel Pedido { get; set; }
        public int idProduto { get; set; }
        public ProdutoModel Produto { get; set; }
        public int qtd { get; set; }
        public float ValorVenda { get; set; }
    }
}
