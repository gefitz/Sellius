using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Interfaces;

namespace RoteiroFacil.API.Services.Produto
{

    public class ProdutoService
    {
        private readonly IRepositoryCRUD<ProdutoModel> _repository;

        public ProdutoService(IRepositoryCRUD<ProdutoModel> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProdutoModel>> ListarClientes(ProdutoModel produto)
        {
            return await _repository.SearchObj(produto);
        }
        public async Task<bool> CadastrarCliente(ProdutoModel produto)
        {
            //produto.dthCriacao = DateTime.Now;
            //produto.dthAlteracao = DateTime.Now;
            return await _repository.Create(produto);
        }
        public async Task<bool> AlterarCadastroCliente(ProdutoModel produto)
        {
            //produto.dthAlteracao = DateTime.Now;
            return await _repository.Update(produto);
        }
        public async Task<bool> InativarCliente(int id)
        {
            ProdutoModel produto = await _repository.GetId(id);
            if (produto == null)
                return false;
            produto.fAtivo = 0;
            return await _repository.Delete(produto);
        }
    }
}
