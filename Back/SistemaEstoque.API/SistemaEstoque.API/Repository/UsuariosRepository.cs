using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.DTOs;

namespace SistemaEstoque.API.Repository
{
    public class UsuariosRepository : IDbMethods<UsuarioModel>
    {
        private readonly AppDbContext _context;

        public UsuariosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(UsuarioModel usuario)
        {
            try
            {
                var cidade = await _context.Cidades.Where(x => x.id == usuario.Cidade.id).FirstOrDefaultAsync();
                if (cidade != null)
                {
                    usuario.Cidade = cidade;
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
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


                var ret = _context.Usuarios.Where(u => u.Email == usuario.Email).FirstOrDefault();
                if (ret != null)
                {
                    return ret;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
