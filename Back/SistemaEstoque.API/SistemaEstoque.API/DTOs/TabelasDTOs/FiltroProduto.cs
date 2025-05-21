namespace SistemaEstoque.API.DTOs.TabelasDTOs
{
    public class FiltroProduto
    {

        public string Nome { get; set; }
        public int tipoProdutoId { get; set; }
        public short fAtivo { get; set; }
        public int FornecedorId { get; set; }
    }
}
