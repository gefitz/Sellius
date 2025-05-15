using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

namespace SistemaEstoque.API.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ObterClientes([FromBody]ClienteDTO clienteDTO)
        {
            IEnumerable<ClienteDTO> ret = await _service.BuscarClientes(clienteDTO);
            if (ret.Count() == 0)
            {
                return BadRequest();
            }
            return Ok(ret);
        }
        public async Task<IActionResult> Cadastrar(int? id)
        {
            ClienteDTO cliente = new ClienteDTO();
            if (id != 0 && id != null)
            {
                cliente = await _service.BuscarId((int)id);
            }
            return View(cliente);
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ClienteDTO cliente)
        {
            var teste  = new StreamReader(HttpContext.Request.Body, Encoding.UTF8,leaveOpen:true);
            var body = await teste.ReadLineAsync();

            ModelState.Remove("Pedidos");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (cliente.id != 0)
            {
                if (await _service.UpdateCliente(cliente)) { return Ok(); }
            }
            else
            {
                var response = await _service.CadastrarCliente(cliente);
                if (response.success)
                     return Ok(response); 
            }
            return BadRequest(Response);

        }
        public async Task<IActionResult> InativarCliente(int id)
        {
            if(await _service.InativarCliente(id))
            {
                return Ok();
            }
            return BadRequest();

        }

    }
}
