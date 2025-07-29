using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Usuarios.Interfaces
{
    public interface IUsuariosRepository :
        IDbMethods<UsuarioModel>,
        IPaginacao<UsuarioModel,UsuarioModel>
    {
    }
}
