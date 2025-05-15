using SistemaEstoque.API.Context;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SistemaEstoque.API.Repository
{
    public class PedidoRepository : IDbMethods<PedidoModel>
    {
        private readonly AppDbContext _context;
        private readonly IDbMethods<ClienteModel> _cliente;
        private readonly IDbMethods<ProdutoModel> _produto;

        public PedidoRepository(AppDbContext context, IDbMethods<ClienteModel> cliente, IDbMethods<ProdutoModel> produto)
        {
            _context = context;
            _cliente = cliente;
            _produto = produto;
        }

        public Task<PedidoModel> BuscaDireto(PedidoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(PedidoModel obj)
        {
            try
            {
                var cliente = await _cliente.BuscaDireto(obj.Cliente);
                //var produto = await _produto.BuscaDireto(obj.Produto);
                //if(produto == null) { _log.Error("Produto não foi selecionado",false); return false; }
                obj.Cliente = cliente;
                //obj.Produto = produto;
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
