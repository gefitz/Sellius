using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Services.Clientes;

namespace RoteiroFacil.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;
        private readonly LogModel _log;

        public ClienteController(ClienteService service, LogModel log)
        {
            _service = service;
            _log = log;
        }
        [HttpPost("ListarClientes")]
        public async Task<ActionResult<IEnumerable<ClienteModel>>> ListarClientes(ClienteModel? cliente)
        {
            var listClientes = await _service.ListarClientes(cliente);
            if (!string.IsNullOrEmpty(_log.Message))
                return BadRequest(_log);

            return Ok(listClientes);
        }
        [HttpPost]
        public async Task<IActionResult> CadastrarCliente(ClienteModel cliente)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            if(await _service.CadastrarCliente(cliente))
                return Ok(cliente);
            return BadRequest(_log);

        }
        [HttpPut]
        public async Task<IActionResult> AlterarCadastroCliente(ClienteModel cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _service.AlterarCadastroCliente(cliente))
                return Ok(cliente);
            return BadRequest(_log);
        }
        [HttpDelete]
        public async Task<IActionResult> InativarCliente(int id)
        {
            if (id == 0)
                return BadRequest("Deve passar o id do cliente");
            if (await _service.InativarCliente(id))
                return Ok();
            return BadRequest(_log);
        }
    }
}
