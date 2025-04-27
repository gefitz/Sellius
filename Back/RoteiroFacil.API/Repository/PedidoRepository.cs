using Microsoft.EntityFrameworkCore;
using RoteiroFacil.API.Context;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Interfaces;
using RoteiroFacil.API.Services.Geral;

namespace RoteiroFacil.API.Repository
{
    public class PedidoRepository : IRepositoryCRUD<PedidosModel>
    {
        private readonly RoteiroFacilContext _context;
        private readonly LogService _log;
        public async Task<bool> Create(PedidosModel obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return true;

            }catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "PedidoRepository");
                return false;
            }
        }

        public Task<bool> Delete(PedidosModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<PedidosModel> GetId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PedidosModel>> SearchObj(PedidosModel obj)
        {
            try
            {
                return await _context.Pedidos.Include(pxp => pxp.PedidoProdutos)
                    .ThenInclude(p => p.Produto)
                    .Where(pe =>
                    (obj.DthPedido == DateTime.MinValue || pe.DthPedido < obj.DthPedido)
                    ).ToListAsync();
            }catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "PedidoRepository");
                return null;
            }
        }

        public Task<bool> Update(PedidosModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
