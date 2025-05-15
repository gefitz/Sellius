using AutoMapper;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Services
{
    public class ClienteService
    {
        private readonly IDbMethods<ClienteModel> _repository;
        private readonly IMapper _mapper;

        public ClienteService(IDbMethods<ClienteModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<ClienteDTO>> CadastrarCliente(ClienteDTO clienteDTO)
        {
            try
            {

                ClienteModel cliente = _mapper.Map<ClienteModel>(clienteDTO);
                if (await _repository.Create(cliente))
                    return Response<ClienteDTO>.Ok();
                return Response<ClienteDTO>.Failed("Falha ao criar o cliente");
            }
            catch (ApplicationException e)
            {
                return Response<ClienteDTO>.Failed(e.Message);
            }
            catch(Exception e)
            {
                return Response<ClienteDTO>.Failed(e.Message);

            }

        }
        public async Task<IEnumerable<ClienteDTO>> BuscarClientes(ClienteDTO? clienteDTO)
        {
            ClienteModel cliente = new ClienteModel();
            cliente.Cidade = new CidadeModel();
            if (clienteDTO != null) { cliente = _mapper.Map<ClienteModel>(clienteDTO); }
            return _mapper.Map<IEnumerable<ClienteDTO>>(await _repository.Filtrar(cliente));
        }
        public async Task<ClienteDTO> BuscarId(int id)
        {
            ClienteModel cliente = new ClienteModel { id = id };
            return _mapper.Map<ClienteDTO>(await _repository.BuscaDireto(cliente));
        }
        public async Task<bool> UpdateCliente(ClienteDTO clienteDTO)
        {
            ClienteModel cliente = _mapper.Map<ClienteModel>(clienteDTO);
            return await _repository.Update(cliente);
        }
        public async Task<bool> InativarCliente(int id)
        {
            ClienteModel cliente = new ClienteModel() { id = id };
            return await _repository.Delete(cliente);
        }
    }
}
