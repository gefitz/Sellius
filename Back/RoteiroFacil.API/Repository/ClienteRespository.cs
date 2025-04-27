using Microsoft.EntityFrameworkCore;
using RoteiroFacil.API.Context;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Interfaces;
using RoteiroFacil.API.Services.Geral;

namespace RoteiroFacil.API.Repository
{
    public class ClienteRespository : IRepositoryCRUD<ClienteModel>
    {
        private readonly RoteiroFacilContext _context;
        private readonly LogService _log;

        public ClienteRespository(RoteiroFacilContext context, LogService log)
        {
            _context = context;
            _log = log;
        }

        public async Task<bool> Create(ClienteModel obj)
        {
            try
            {
                _context.Clientes.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "ClienteRepository");
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
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "ClienteRepository");
                return false;
            }
        }

        public async Task<ClienteModel> GetId(int id)
        {
            try
            {
                return await _context.Clientes.Where(c => c.Id == id).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "ClienteRepository");
                return null;
            }
        }

        public async Task<List<ClienteModel>> SearchObj(ClienteModel obj)
        {
            try
            {
                return await _context.Clientes
                    .Include(r => r.Represetante)
                    .ThenInclude(u => u.Usuario)
                    .Where(cliente =>
                    cliente.Represetante.id == obj.Represetante.id
                    &&
                    (string.IsNullOrEmpty(obj.Nome) || cliente.Nome == obj.Nome)
                    &&
                    (string.IsNullOrEmpty(obj.Documento) || cliente.Documento == obj.Documento)
                    )
                    .Include(p => p.Pedidos)
                    .ThenInclude(pxp => pxp.PedidoProdutos)
                    .ThenInclude(pedidoXProduto => pedidoXProduto.Produto)
                    .Include(roteiro => roteiro.Roteiro)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "ClienteRepository");
                return null;
            }
        }

        public async Task<bool> Update(ClienteModel obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "ClienteRepository");
                return false;
            }
        }
    }
}
