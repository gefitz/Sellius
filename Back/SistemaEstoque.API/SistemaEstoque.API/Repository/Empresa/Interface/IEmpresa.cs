using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Empresa.Interface
{
    public interface IEmpresa:
        IDbMethods<EmpresaModel>,
        IPaginacao<EmpresaModel, EmpresaModel>
    {
    }
}
