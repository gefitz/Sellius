using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.Repository.Produto.Interface;
using SistemaEstoque.API.DTOs.TabelasDTOs;

namespace SistemaEstoque.API.Repository.Produto
{
    public class TpProdutoRepository : ITpProdutoRepository
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;
        public TpProdutoRepository(AppDbContext context,LogRepository log)
        {
            _log = log;
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
                _log.Error(ex);
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
                _log.Error(ex);
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
                _log.Error(ex);
                return false;
            }
        }

        public async Task<IEnumerable<TipoProdutoModel>> Filtrar(TipoProdutoModel obj)
        {
            try
            {
                return await _context.TpProdutos.Where(tp =>
                    string.IsNullOrEmpty(obj.Tipo) || tp.Tipo == obj.Tipo
                    ).ToListAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

        public async Task<PaginacaoTabelaResult<TipoProdutoModel, TipoProdutoModel>> Filtrar(PaginacaoTabelaResult<TipoProdutoModel, TipoProdutoModel> obj)
        {
            try
            {

                var query = _context.TpProdutos.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.Tipo))
                    query = query.Where(p => p.Tipo.Contains(obj.Filtro.Tipo));
                if (!string.IsNullOrEmpty(obj.Filtro.Descricao)) 
                    query = query.Where(p => p.Descricao.Equals(obj.Filtro.Descricao));
                if (obj.Filtro.fAtivo != 0) 
                    query = query.Where(p => p.fAtivo.Equals(obj.Filtro.fAtivo));

                query.Where(p => p.Empresaid == obj.Filtro.Empresaid);

                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .ToListAsync();

                return obj;
            }
            catch(Exception ex)
            {
                _log.Error(ex); 
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
                _log.Error(ex);
                return false;
            }
        }
    }
}
