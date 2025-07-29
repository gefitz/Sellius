using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Login.Interfaces
{
    public interface ILoginRepository : IDbMethods<LoginModel>
    {
        public Task<bool> VereficaEmailExistente(LoginModel model);
    }
}
