using SistemaEstoque.API.Models.Cliente;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Cliente.Interfaces
{
    public interface IGrupoClientesRepository : IDbMethods<GrupoClienteModel>, IPaginacao<GrupoClienteModel, GrupoClienteModel>
    {
        Task<List<GrupoClienteModel>> CarregarCombo(int idEmpresa);
    }
}
