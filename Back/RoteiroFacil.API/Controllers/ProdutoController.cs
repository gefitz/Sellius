using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Services.Produto;

namespace RoteiroFacil.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _service;
        private readonly LogModel _log;

        public ProdutoController(ProdutoService service, LogModel log)
        {
            _service = service;
            _log = log;
        }

        [HttpPost("ListarProdutos")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> ListarClientes(ProdutoModel? produto)
        {
            var listClientes = await _service.ListarClientes(produto);
            if (!string.IsNullOrEmpty(_log.Message))
                return BadRequest(_log);

            return Ok(listClientes);
        }
        [HttpPost]
        public async Task<IActionResult> CadastrarCliente(ProdutoModel produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _service.CadastrarCliente(produto))
                return Ok(produto);
            return BadRequest(_log);

        }
        [HttpPut]
        public async Task<IActionResult> AlterarCadastroCliente(ProdutoModel produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _service.AlterarCadastroCliente(produto))
                return Ok(produto);
            return BadRequest(_log);
        }
        [HttpDelete]
        public async Task<IActionResult> InativarCliente(int id)
        {
            if (id == 0)
                return BadRequest("Deve passar o id do produto");
            if (await _service.InativarCliente(id))
                return Ok();
            return BadRequest(_log);
        }

    }
}
