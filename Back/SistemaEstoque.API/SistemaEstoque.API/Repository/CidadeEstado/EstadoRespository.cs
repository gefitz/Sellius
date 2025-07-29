using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using SistemaEstoque.API.Services;

namespace SistemaEstoque.API.Repository.CidadeEstado
{
    public class EstadoRespository : IDbMethods<EstadoModel>
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;

        public EstadoRespository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public Task<EstadoModel> BuscaDireto(EstadoModel idObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(EstadoModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(EstadoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EstadoModel>> Filtrar(EstadoModel obj)
        {
            try
            {
                return await _context.Estados.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Update(EstadoModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
