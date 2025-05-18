using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository
{
    public class LoginRepository : IDbMethods<LoginModel>
    {
        private AppDbContext _context;
        private LogRepository _log;

        public LoginRepository(AppDbContext context,LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public Task<LoginModel> BuscaDireto(LoginModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(LoginModel obj)
        {
            try
            {
                _context.Logins.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }
        }

        public Task<bool> Delete(LoginModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoginModel>> Filtrar(LoginModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(LoginModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
