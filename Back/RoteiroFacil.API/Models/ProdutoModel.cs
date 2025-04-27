namespace RoteiroFacil.API.Models
{
    public class ProdutoModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public float preco { get; set; }
        public int qtd { get; set; }
        public short fAtivo { get; set; }
        public List<PedidoProdutoModel> PedidoProdutos { get; set; }
    }
}