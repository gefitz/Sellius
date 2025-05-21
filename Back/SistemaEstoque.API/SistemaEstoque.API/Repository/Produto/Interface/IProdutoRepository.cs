using SistemaEstoque.API.DTOs.TabelasDTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Produto.Interface
{
    public interface IProdutoRepository
        :
        IDbMethods<ProdutoModel>,
        IPaginacao<ProdutoModel,FiltroProduto>
    {
    }
}
