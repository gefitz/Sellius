using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SistemaEstoque.API.Repository
{
    public class ProdutoRepository : IDbMethods<ProdutoModel>
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProdutoModel> BuscaDireto(ProdutoModel obj)
        {

            try
            {

                var produto = await _context.Produtos.Include(tp => tp.tipoProduto).Where(p => p.id == obj.id).FirstOrDefaultAsync();
                if(produto != null)
                {
                    return produto;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Create(ProdutoModel obj)
        {
            try
            {
                var tpProduto = await _context.TpProdutos.Where(tp => tp.id == obj.tipoProduto.id).FirstOrDefaultAsync();
                if (tpProduto != null)
                {
                    obj.tipoProduto = tpProduto;
                }
                _context.Produtos.Add(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(ProdutoModel obj)
        {
            try
            {
                _context.Produtos.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProdutoModel>> Filtrar(ProdutoModel obj)
        {
            try
            {

                return await _context.Produtos
                    .Include(tp => tp.tipoProduto)
                    //.Include(e => e.Empresa)
                    //.Include(f => f.Fornecedor)
                    //.Where(p =>
                    //    (string.IsNullOrEmpty(obj.Nome) || p.Nome.Contains(obj.Nome))
                    //    &&
                    //    (string.IsNullOrEmpty(obj.Descricao) || p.Descricao.Contains(obj.Descricao))
                    //    //&&
                    //    //(string.IsNullOrEmpty(obj.TpProduto.Tipo) || p.TpProduto.Tipo.Contains(obj.TpProduto.Tipo))
                    //)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Update(ProdutoModel obj)
        {
            try
            {
                var tp = await _context.TpProdutos.Where(tp => tp.id == obj.tipoProduto.id).FirstOrDefaultAsync();
                if (tp != null)
                    obj.tipoProduto = tp;
                _context.Produtos.Entry(obj).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
