using AutoMapper;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.DTOs.CadastrosDTOs;
using SistemaEstoque.API.DTOs.TabelasDTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using SistemaEstoque.API.Repository.Produto;
using SistemaEstoque.API.Repository.Produto.Interface;

namespace SistemaEstoque.API.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;


        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> CadastrarProduto(ProdutoDTO produtoDTO)
        {
            ProdutoModel produto = produtoDTO;
            if (await _repository.Create(produto))
                return true;
            return false;
        }
        public async Task<Response<PaginacaoTabelaResult<ProdutoDTO,FiltroProduto>>> FiltrarProduto(PaginacaoTabelaResult<ProdutoDTO,FiltroProduto> paginacao)
        {
            PaginacaoTabelaResult<ProdutoModel, FiltroProduto> p = new PaginacaoTabelaResult<ProdutoModel, FiltroProduto>
            {
                TamanhoPagina = paginacao.TamanhoPagina,
                TotalPaginas = paginacao.TotalPaginas,
                Filtro = paginacao.Filtro,
                PaginaAtual = paginacao.PaginaAtual,
            };

            var paginacaoResult = await _repository.Filtrar(p);
            
            List<ProdutoDTO> listaProduto = ProdutoDTO.FromModelList(paginacaoResult.Dados);

            var result = new PaginacaoTabelaResult<ProdutoDTO, FiltroProduto>
            {
                TamanhoPagina = paginacaoResult.TamanhoPagina,
                TotalPaginas = paginacaoResult.TotalPaginas,
                PaginaAtual = paginacaoResult.PaginaAtual,
                Dados = listaProduto,
                TotalRegistros = paginacaoResult.TotalRegistros,
            };
            return Response<PaginacaoTabelaResult<ProdutoDTO, FiltroProduto>>.Ok(result);

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
            if (await _repository.Update(produto)) return true;
            return false;
        }
    }
}
