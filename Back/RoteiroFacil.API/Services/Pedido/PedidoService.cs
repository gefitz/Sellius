using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Interfaces;

namespace RoteiroFacil.API.Services.Pedido
{
    public class PedidoService
    {
        private readonly IRepositoryCRUD<PedidosModel> _repository;
        private readonly PedidoXProdutoService _servicePedidoXProduto;

        public PedidoService(IRepositoryCRUD<PedidosModel> repository, PedidoXProdutoService servicePedidoXProduto)
        {
            _repository = repository;
            _servicePedidoXProduto = servicePedidoXProduto;
        }
        public async Task<IEnumerable<PedidosModel>> ListarPedido(PedidosModel produto)
        {
            return await _repository.SearchObj(produto);
        }
        public async Task<bool> GerarPedido(PedidosModel produto)
        {
            //produto.dthCriacao = DateTime.Now;
            //produto.dthAlteracao = DateTime.Now;
            return await _repository.Create(produto);
        }
        public async Task<bool> FinalizarPedido(PedidosModel produto)
        {
            //produto.dthAlteracao = DateTime.Now;
            return await _repository.Update(produto);
        }

    }
}
