using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SistemaEstoque.API.Repository
{
    public class PedidoRepository : IDbMethods<PedidoModel>
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;

        }

        public Task<PedidoModel> BuscaDireto(PedidoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(PedidoModel obj)
        {
            try
            {
                _context.Pedidos.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> Delete(PedidoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PedidoModel>> Filtrar(PedidoModel obj)
        {
            try
            {
                return await _context.Pedidos
                    //.Include(p => p.Produto).ThenInclude(tp => tp.TpProduto)
                    .Include(c => c.Cliente).ThenInclude(c => c.Cidade)
                    .Where(pe =>
                    //(string.IsNullOrEmpty(obj.Produto.Nome) || pe.Produto.Nome == obj.Produto.Nome)
                    //&&
                    (string.IsNullOrEmpty(obj.Cliente.Nome) || pe.Cliente.Nome == obj.Cliente.Nome)
                    ).ToListAsync();
            }catch (Exception ex)
            {
                return null;
            }
        }

        public Task<bool> Update(PedidoModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
