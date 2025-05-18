using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Services;

namespace SistemaEstoque.API.Repository
{
    public class UsuariosRepository : IDbMethods<UsuarioModel>
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;

        public UsuariosRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<bool> Create(UsuarioModel usuario)
        {
            try
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }

        }

        public Task<bool> Delete(UsuarioModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioModel>> Filtrar(UsuarioModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UsuarioModel obj)
        {
            throw new NotImplementedException();
        }
        public async Task<UsuarioModel> BuscaDireto(UsuarioModel usuario)
        {
            try
            {
                UsuarioModel ret = new UsuarioModel();
                if (usuario.EmpresaId != 0)
                {

                    ret = await _context.Usuarios.Where(u => u.Email == usuario.Email && u.EmpresaId == usuario.EmpresaId).FirstOrDefaultAsync();
                    if (ret != null)
                    {
                        return ret;
                    }
                }
                else
                {
                    ret = await _context.Usuarios.Where(u => u.Email == usuario.Email).FirstOrDefaultAsync();
                    if (ret != null)
                    {
                        return ret;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

    }
}
