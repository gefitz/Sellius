using AutoMapper;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.DTOs.CadastrosDTOs;
using SistemaEstoque.API.DTOs.TabelasDTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using SistemaEstoque.API.Repository.Produto.Interface;

namespace SistemaEstoque.API.Services
{
    public class TpProdutoService
    {
        private readonly ITpProdutoRepository _repository;

        public TpProdutoService(ITpProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
        }
        public async Task<Response<TipoProdutoDTO>> CadastrarTpProduto(TipoProdutoDTO dto)
        {
            TipoProdutoDTO model = dto;
            if (await _repository.Create(model))
                return Response<TipoProdutoDTO>.Ok(model);
            return Response<TipoProdutoDTO>.Failed("Falha ao criar um novo tipo produto");

        }
        public async Task<Response<PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO>>> BuscarTpProudo(PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO> dto)
        {
            try
            {

                PaginacaoTabelaResult<TipoProdutoModel, TipoProdutoModel> model = new PaginacaoTabelaResult<TipoProdutoModel, TipoProdutoModel>
                {
                    TotalRegistros = dto.TotalRegistros,
                    TotalPaginas = dto.TotalPaginas,
                    TamanhoPagina = dto.TamanhoPagina,
                    Filtro = dto.Filtro,
                    PaginaAtual = dto.PaginaAtual

                };
                var result = await _repository.Filtrar(model);
                dto.Dados = TipoProdutoDTO.FromList(result.Dados);
                return Response<PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO>>.Failed(ex.Message);
            }
        }
        public async Task<Response<TipoProdutoDTO>> BuscarId(int id)
        {
            try
            {

                TipoProdutoModel cliente = new TipoProdutoModel { id = id };
                return Response<TipoProdutoDTO>.Ok(await _repository.BuscaDireto(cliente));
            }
            catch (Exception ex)
            {
                return Response<TipoProdutoDTO>.Failed(ex.Message);
            }
        }
        public async Task<Response<TipoProdutoDTO>> UpdateTpProduto(TipoProdutoDTO dto)
        {
            TipoProdutoModel model = dto;
            if(await _repository.Update(model))
                return Response<TipoProdutoDTO>.Ok(model);
            return Response<TipoProdutoDTO>.Failed("Falha ao fazer modificação");
        }
        public async Task<Response<TipoProdutoDTO>> InativarTpProduto(TipoProdutoDTO dto)
        {
            var model = await BuscarId(dto.id);
            if (!model.success) {
                return Response<TipoProdutoDTO>.Failed("O id desse tipo produto não foi encontrado");
            }
            return await UpdateTpProduto(dto);
        }
    }
}
