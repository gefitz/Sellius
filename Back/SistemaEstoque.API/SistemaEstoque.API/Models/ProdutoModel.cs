using SistemaEstoque.API.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.Models
{
    public class ProdutoModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoProdutoModel tipoProduto { get; set; }
        public float valor { get; set; }
        public int qtd { get; set; }
        public DateTime dthCriacao { get; set; }
        public DateTime dthAlteracao { get; set; }
        public IEnumerable<PedidoXProduto> pedidos { get; set; }
        public int fAtivo { get; set; }
        public FornecedoresModel Fornecedor { get; set; }
        public EmpresaModel Empresa { get; set; }

        public static implicit operator ProdutoModel(FiltroProduto filtroProduto)
        {
            return new ProdutoModel
            {
                Nome = filtroProduto.Nome,
                tipoProduto = new TipoProdutoModel { id = filtroProduto.tipoProduto },

            };
        }

    }
}
