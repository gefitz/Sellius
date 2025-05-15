using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace SistemaEstoque.API.Controllers
{
    public class TpProdutoController : Controller
    {
        private readonly TpProdutoService _service;

        public TpProdutoController(TpProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TipoProdutoDTO> tpProduto = await _service.BuscarTpProudo(new TipoProdutoDTO());
            if (tpProduto.Count() == 0)
            {
                return BadRequest();
            }
            ViewBag.TpProduto = tpProduto;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ObterTpProduto([FromBody]TipoProdutoDTO tipoProdutoDTO)
        {
            ModelState.Remove("TpProduto");
            ModelState.Remove("Descricao");
            IEnumerable<TipoProdutoDTO> ret = await _service.BuscarTpProudo(tipoProdutoDTO);
            if (ret.Count() == 0)
            {
                return BadRequest();
            }
            return Ok(ret);
        }
        [HttpPost]
        public async Task<IActionResult> CadastrarTpProduto([FromBody]TipoProdutoDTO tipoProduto)
        {
            var teste = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await teste.ReadToEndAsync();
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (tipoProduto.id != 0)
            {
                if (await _service.UpdateTpProduto(tipoProduto)) { return Ok(); }
            }
            else
            {
                if (await _service.CadastrarTpProduto(tipoProduto)) { return Ok(); }
            }
            return BadRequest();


        }
        public async Task<IActionResult> InativarTpProduto(int id)
        {
            if (await _service.InativarTpProduto(id))
            {
                return Ok();
            }
            //ViewBag.Error = _log.Messagem;
            return BadRequest();

        }
    }
}
