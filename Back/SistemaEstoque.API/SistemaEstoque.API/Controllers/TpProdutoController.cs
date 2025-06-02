using SistemaEstoque.API.Models;
using SistemaEstoque.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using SistemaEstoque.API.DTOs.CadastrosDTOs;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.DTOs.TabelasDTOs;
using Microsoft.AspNetCore.Authorization;
namespace SistemaEstoque.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class TpProdutoController : Controller
    {
        private readonly TpProdutoService _service;

        public TpProdutoController(TpProdutoService service)
        {
            _service = service;
        }
        [HttpPost("ObterTabelaTpProduto")]
        public async Task<IActionResult> ObterTpProduto(PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO> tipoProdutoDTO)
        {
            var ret = await _service.BuscarTpProudo(tipoProdutoDTO);
            if (!ret.success)
            {
                return BadRequest(ret);
            }
            return Ok(ret);
        }
        [HttpPost("NovoTpProduto")]
        public async Task<IActionResult> CadastrarTpProduto([FromBody] TipoProdutoDTO tipoProduto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<TipoProdutoDTO>.Failed(menssagemErro));
            }
            var ret = await _service.CadastrarTpProduto(tipoProduto);
            if (ret.success) { return Ok(ret); }
            return BadRequest(ret);


        }
        [HttpPut]
        public async Task<IActionResult> UpdateTpProduto(TipoProdutoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<TipoProdutoDTO>.Failed(menssagemErro));
            }
            var result = await _service.UpdateTpProduto(dto);
            if (result.success) { return Ok(result); }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> InativarTpProduto(TipoProdutoDTO dto)
        {


            var result = await _service.InativarTpProduto(dto);
            if (result.success)
            {
                return Ok(result);
            }
            //ViewBag.Error = _log.Messagem;
            return BadRequest(result);

        }
    }
}
