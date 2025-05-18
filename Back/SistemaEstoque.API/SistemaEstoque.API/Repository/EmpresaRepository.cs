using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.Context;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository
{
    public class EmpresaRepository : IDbMethods<EmpresaModel>
    {
        private AppDbContext _context;
        private LogRepository _log;

        public EmpresaRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<EmpresaModel> BuscaDireto(EmpresaModel obj)
        {
            try
            {
                return await _context.Empresas.Where(e => e.CNPJ.Equals(obj.CNPJ)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

        public async Task<bool> Create(EmpresaModel obj)
        {
            try
            {
                _context.Empresas.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }
        }

        public Task<bool> Delete(EmpresaModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmpresaModel>> Filtrar(EmpresaModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(EmpresaModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
