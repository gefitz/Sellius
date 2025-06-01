using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Login.Interfaces
{
    public interface ILogin : IDbMethods<LoginModel>
    {
        public Task<bool> VereficaEmailExistente(LoginModel model);
    }
}
