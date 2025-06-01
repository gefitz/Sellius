using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.DTOs.TabelasDTOs;
using SistemaEstoque.API.Repository.Produto.Interface;
using SistemaEstoque.API.DTOs.Filtros;

namespace SistemaEstoque.API.Repository.Produto
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;
        public ProdutoRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
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
                _context.Produtos.Add(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _log.Error(ex);
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

        public async Task<PaginacaoTabelaResult<ProdutoModel,FiltroProduto>> Filtrar(PaginacaoTabelaResult<ProdutoModel,FiltroProduto> obj)
        {
            try
            {

                var query = _context.Produtos.AsQueryable();

                if(!string.IsNullOrEmpty(obj.Filtro.Nome))
                    query = query.Where(p => p.Nome.Contains(obj.Filtro.Nome));
                if(obj.Filtro.tipoProdutoId != 0)
                    query = query.Where(p => p.TipoProdutoId.Equals(obj.Filtro.tipoProdutoId));
                if (obj.Filtro.fAtivo != 0)
                    query = query.Where(p => p.fAtivo.Equals(obj.Filtro.fAtivo));
                if(obj.Filtro.FornecedorId != 0)
                    query = query.Where(p => p.FornecedorId.Equals(obj.Filtro.FornecedorId));
                query.Where(p => p.EmpresaId == 0);
                
                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .ToListAsync();

                return obj;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

        public Task<IEnumerable<ProdutoModel>> Filtrar(ProdutoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ProdutoModel obj)
        {
            try
            {
                var tp = await _context.TpProdutos.Where(tp => tp.id == obj.tipoProduto.id).FirstOrDefaultAsync();
                if (tp != null)
                    obj.tipoProduto = tp;
                _context.Produtos.Entry(obj).State = EntityState.Modified;
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
