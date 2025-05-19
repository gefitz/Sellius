using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository
{
    public class LoginRepository : IDbMethods<LoginModel>
    {
        private AppDbContext _context;
        private LogRepository _log;

        public LoginRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<LoginModel> BuscaDireto(LoginModel obj)
        {
            try
            {
                return await _context.Logins.Include(u => u.Usuario).Where(l => l.Email == obj.Email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
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
