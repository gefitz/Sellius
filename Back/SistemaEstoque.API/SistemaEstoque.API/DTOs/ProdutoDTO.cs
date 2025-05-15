using SistemaEstoque.API.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.DTOs
{
    public class ProdutoDTO
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Necessario o Nome do Produto")]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Necessario a Descrição do Produto")]
        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set;}

        [Required(ErrorMessage = "Necessario a Tipo do Produto")]
        [Display(Name = "Tipo do Produto")]
        public TipoProdutoDTO tipoProduto { get; set; }

        [Required(ErrorMessage = "Necessario a Valor do Produto")]
        [Display(Name = "Valor do Produto")]
        public float valor { get; set; }

        [Required(ErrorMessage = "Necessario a Quantidade do Produto")]
        [Display(Name = "Quantidade do Produto")]
        public int qtd { get; set; }

        public DateTime dthCriacao { get; set; }
        public DateTime dthAlteracao { get; set; }
        public int fAtivo { get; set; }
        public FornecedoresModel Fornecedor { get; set; }
        public EmpresaModel Empresa { get; set; }



        #region Mapper
        public static implicit operator ProdutoDTO(ProdutoModel model)
        {
            return new ProdutoDTO
            {
                id = model.id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                tipoProduto = model.tipoProduto,
                valor = model.valor,
                qtd = model.qtd,
                dthCriacao = model.dthCriacao,
                dthAlteracao = model.dthAlteracao,
                Fornecedor = model.Fornecedor,
                Empresa = model.Empresa,
            };
        }

        public static IEnumerable<ProdutoDTO> FromModelList(IEnumerable<ProdutoModel> produtosModel)
        {
            List<ProdutoDTO> dtoList = new List<ProdutoDTO>();
            foreach (var item in produtosModel)
            {
                dtoList.Add(item);
            }
            return dtoList;
        }
        #endregion

    }
    public class FiltroProduto
    {
        public string Nome { get; set; }
        public int tipoProduto { get; set; }
        public int marca { get; set; }
        public short fAtivo { get; set; }
        
    }

}
