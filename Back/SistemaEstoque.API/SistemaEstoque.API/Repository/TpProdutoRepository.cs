using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SistemaEstoque.API.Repository
{
    public class TpProdutoRepository : IDbMethods<TipoProdutoModel>
    {
        private readonly AppDbContext _context;
        public TpProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TipoProdutoModel> BuscaDireto(TipoProdutoModel obj)
        {
            try
            {
                return await _context.TpProdutos.Where(tp => tp.id == obj.id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Create(TipoProdutoModel obj)
        {
            try
            {
                _context.TpProdutos.Add(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(TipoProdutoModel obj)
        {
            try
            {
                var tpProduto = await BuscaDireto(obj);
                if (tpProduto == null)
                {
                    return false;
                }
                _context.TpProdutos.Remove(tpProduto);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TipoProdutoModel>> Filtrar(TipoProdutoModel obj)
        {
            try
            {
                return await _context.TpProdutos.Where(tp =>
                    (string.IsNullOrEmpty(obj.Tipo) || tp.Tipo == obj.Tipo)
                    ).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Update(TipoProdutoModel obj)
        {
            try
            {
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
