using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SistemaEstoque.API.Repository
{
    public class ClienteRepository : IDbMethods<ClienteModel>
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteModel> BuscaDireto(ClienteModel obj)
        {
            try
            {
                return await _context.Clientes
                    .Include(c => c.Cidade)
                    .Include(p => p.Pedidos)
                    .ThenInclude(p => p.Produto)
                    .Where(c => c.id == obj.id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Create(ClienteModel obj)
        {
            try
            {
                var cidade = _context.Cidades.Where(c => c.id == obj.Cidade.id).FirstOrDefault();
                if (cidade != null)
                {
                    obj.Cidade = cidade;
                }
                _context.Clientes.Add(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Falha ao cadastrar o clinete " + obj.Nome + ".\n erro: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(ClienteModel obj)
        {
            try
            {
                _context.Clientes.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ClienteModel>> Filtrar(ClienteModel obj)
        {
            try
            {

                return await _context.Clientes
                    .Include(c => c.Cidade)
                    .Include(pe => pe.Pedidos)
                        .ThenInclude(p => p.Produto)
                     .Where(c =>
                     (string.IsNullOrEmpty(obj.Nome) || c.Nome == obj.Nome)
                     &&
                     (string.IsNullOrEmpty(obj.Documento) || c.Documento == obj.Documento)
                     //&&
                     //(obj.Cidade.Cidade == null || c.Cidade.Cidade == obj.Cidade.Cidade)
                     ).ToListAsync();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ClienteModel>();
            }
        }

        public async Task<bool> Update(ClienteModel obj)
        {
            try
            {
                var cidadeCliente = obj.Cidade;
                if (cidadeCliente.id == 0)
                {
                    var cidade = _context.Cidades.Where(c => c.Cidade.Contains(obj.Cidade.Cidade)).FirstOrDefault();
                    if (cidade == null)
                    {
                        _context.Cidades.Add(cidadeCliente);
                        await _context.SaveChangesAsync();
                        obj.Cidade = cidadeCliente;
                    }
                    else
                    {
                        obj.Cidade = cidade;
                    }
                }
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
