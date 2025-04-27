using Microsoft.EntityFrameworkCore;
using RoteiroFacil.API.Context;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Interfaces;
using RoteiroFacil.API.Services.Geral;

namespace RoteiroFacil.API.Repository
{
    public class ProdutoRepository : IRepositoryCRUD<ProdutoModel>
    {
        private readonly RoteiroFacilContext _context;
        private readonly LogService _logService;

        public ProdutoRepository(RoteiroFacilContext context, LogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<bool> Create(ProdutoModel obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _logService.RegistrarInnerException(ex, "ProdutoRepository");
                return false;
            }
        }

        public async Task<bool> Delete(ProdutoModel obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logService.RegistrarInnerException(ex, "ProdutoRepository");
                return false;
            }
        }

        public async Task<ProdutoModel> GetId(int id)
        {
            try
            {
                return await _context.Produtos
                    .Include(p => p.PedidoProdutos)
                    .ThenInclude(pxp => pxp.Pedido)
                    .Where(p => p.id == id).FirstOrDefaultAsync();               
            }
            catch (Exception ex)
            {
                _logService.RegistrarInnerException(ex, "ProdutoRepository");
                return null;
            }
        }

        public async Task<List<ProdutoModel>> SearchObj(ProdutoModel obj)
        {
            try
            {
                return await _context.Produtos
                    .Include(p => p.PedidoProdutos)
                    .ThenInclude(pxp => pxp.Pedido)
                    .Where(p =>
                    (string.IsNullOrEmpty(obj.Nome) || p.Nome == obj.Nome)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logService.RegistrarInnerException(ex, "ProdutoRepository");
                return null;
            }
        }

        public async Task<bool> Update(ProdutoModel obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logService.RegistrarInnerException(ex, "ProdutoRepository");
                return false;
            }
        }
    }
}
