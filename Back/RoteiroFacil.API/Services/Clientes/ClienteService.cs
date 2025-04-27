using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Interfaces;

namespace RoteiroFacil.API.Services.Clientes
{
    public class ClienteService
    {
        private readonly IRepositoryCRUD<ClienteModel> _repository;

        public ClienteService(IRepositoryCRUD<ClienteModel> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ClienteModel>> ListarClientes(ClienteModel cliente)
        {
            return await _repository.SearchObj(cliente);
        }
        public  async Task<bool> CadastrarCliente(ClienteModel cliente)
        {
            cliente.dthCriacao = DateTime.Now;
            cliente.dthAlteracao = DateTime.Now;
            return await _repository.Create(cliente);
        }
        public async Task<bool> AlterarCadastroCliente(ClienteModel cliente)
        {
            cliente.dthAlteracao = DateTime.Now;
            return await _repository.Update(cliente);
        }
        public async Task<bool> InativarCliente(int id)
        {
            ClienteModel cliente = await _repository.GetId(id);
            if(cliente == null)
                return false;
            return await _repository.Delete(cliente);
        }
    }
}
