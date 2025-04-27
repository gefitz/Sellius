using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Services.Pedido;

namespace RoteiroFacil.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly PedidoService _service;
        private readonly LogModel _log;

        public PedidoController(PedidoService service, LogModel log)
        {
            _service = service;
            _log = log;
        }

        [HttpPost("ListarPedido")]
        public async Task<ActionResult<IEnumerable<PedidosModel>>> ListarPedidos(PedidosModel? pedido)
        {
            var listClientes = await _service.ListarPedido(pedido);
            if (!string.IsNullOrEmpty(_log.Message))
                return BadRequest(_log);

            return Ok(listClientes);
        }
        [HttpPost]
        public async Task<IActionResult> CadastrarPedido(PedidosModel pedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _service.GerarPedido(pedido))
                return Ok(pedido);
            return BadRequest(_log);

        }
        [HttpPost]
        public async Task<IActionResult> FinalizarPedido(PedidosModel pedidos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _service.FinalizarPedido(pedidos))
                return Ok(pedidos);
            return BadRequest(_log);
        }

    }
}
