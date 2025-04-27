using Microsoft.EntityFrameworkCore;
using RoteiroFacil.API.Context;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Interfaces;
using RoteiroFacil.API.Services.Geral;

namespace RoteiroFacil.API.Repository
{
    public class UsuarioRepository : IRepositoryCRUD<UsuarioModel>
    {
        private readonly RoteiroFacilContext _context;
        private readonly LogService _log;

        public UsuarioRepository(RoteiroFacilContext context, LogService log)
        {
            _context = context;
            _log = log;
        }

        public async Task<bool> Create(UsuarioModel obj)
        {
            try
            {
                _context.Usuarios.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "UsuarioRepository");
                return false;
            }
        }

        public Task<bool> Delete(UsuarioModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioModel> GetId(int id)
        {
            return await _context.Usuarios.Where(u => u.id == id).FirstOrDefaultAsync();
        }

        public async Task<List<UsuarioModel>> SearchObj(UsuarioModel obj)
        {
            try
            {
                return await _context.Usuarios.Include(l => l.Licenca)
                    .Include(r => r.Represetante)
                    .Where(u => u.Email.Contains(obj.Email))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "UsuarioRepository");
                return null;
            }
        }

        public async Task<bool> Update(UsuarioModel obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "UsuarioRepository");
                return false;
            }
        }
    }
}
