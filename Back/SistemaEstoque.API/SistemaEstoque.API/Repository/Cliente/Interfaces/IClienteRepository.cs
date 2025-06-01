using SistemaEstoque.API.DTOs.Filtros;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Cliente.Interfaces
{
    public interface IClienteRepository : 
        IDbMethods<ClienteModel>,
        IPaginacao<ClienteModel,FiltroCliente>
    {
    }
}
