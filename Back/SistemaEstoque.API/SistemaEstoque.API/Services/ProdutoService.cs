using AutoMapper;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Services
{
    public class ProdutoService
    {
        private readonly IDbMethods<ProdutoModel> _repository;
        private readonly IMapper _mapper;

        public ProdutoService(IDbMethods<ProdutoModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> CadastrarProduto(ProdutoDTO produtoDTO)
        {
            ProdutoModel produto = _mapper.Map<ProdutoModel>(produtoDTO);
            if (await _repository.Create(produto))
                return true;
            return false;
        }
        public async Task<Response<IEnumerable<ProdutoDTO>>> FiltrarProduto(FiltroProduto produto)
        {
            if (produto == null) { produto = new FiltroProduto(); }
            var produtos = await _repository.Filtrar(produto);
            return Response<IEnumerable<ProdutoDTO>>.Ok(ProdutoDTO.FromModelList(produtos));
        }
        public async Task<bool> InativarProduto(int id)
        {
            ProdutoModel produto = new ProdutoModel() { id = id };
            produto = await _repository.BuscaDireto(produto);
            if (produto == null) { return false; }
            if (await _repository.Delete(produto)) return true;
            return false;
        }
        public async Task<bool> Update(ProdutoDTO produto)
        {
            if (await _repository.Update(_mapper.Map<ProdutoModel>(produto))) return true;
            return false;
        }
        public async Task<ProdutoDTO> ObterIdProduto(int? id)
        {
            return _mapper.Map<ProdutoDTO>(await _repository.BuscaDireto(new ProdutoModel
            {
                id = (int)id
            }));
        }
    }
}
