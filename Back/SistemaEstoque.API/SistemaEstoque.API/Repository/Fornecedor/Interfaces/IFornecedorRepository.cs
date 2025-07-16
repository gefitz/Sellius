using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Fornecedor.Interfaces
{
    public interface IFornecedorRepository:
        IDbMethods<FornecedoresModel>,
        IPaginacao<FornecedoresModel,FornecedoresModel>
    {
        public Task<List<FornecedoresModel>> CarregarComboFornecedor(FornecedoresModel model);
    }
}
