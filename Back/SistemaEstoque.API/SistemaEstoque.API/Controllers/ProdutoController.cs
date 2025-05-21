using SistemaEstoque.API.Models;
using SistemaEstoque.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.API.DTOs.CadastrosDTOs;
using SistemaEstoque.API.DTOs.TabelasDTOs;

namespace SistemaEstoque.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }
        [HttpPost("ObterProduto")]
        public async Task<IActionResult> ObterProduto(PaginacaoTabelaResult<ProdutoDTO,FiltroProduto> produto)
        {
            var response = await _service.FiltrarProduto(produto);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("CadastrarProduto")]
        public async Task<IActionResult> CadastrarProduto(ProdutoDTO produtoDTO)
        {
            ModelState.Remove("TpProduto");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _service.CadastrarProduto(produtoDTO))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduto(ProdutoDTO produtoDTO)
        {
            ModelState.Remove("TpProduto");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _service.Update(produtoDTO))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> InativarProduto(int id)
        {
            if (await _service.InativarProduto(id))
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
