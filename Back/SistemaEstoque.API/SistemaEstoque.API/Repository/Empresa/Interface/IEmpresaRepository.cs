using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Empresa.Interface
{
    public interface IEmpresaRepository:
        IDbMethods<EmpresaModel>,
        IPaginacao<EmpresaModel, EmpresaModel>
    {
    }
}
