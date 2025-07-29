using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Produto.Interface
{
    public interface ITpProdutoRepository :
        IDbMethods<TipoProdutoModel>,
        IPaginacao<TipoProdutoModel,TipoProdutoModel>
    {
        Task<List<TipoProdutoModel>> CarregarCombo(int idEmpresa);
    }
}
