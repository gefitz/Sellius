using SistemaEstoque.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.API.DTOs.CadastrosDTOs;
using SistemaEstoque.API.DTOs.TabelasDTOs;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.DTOs.Filtros;

namespace SistemaEstoque.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }
        [HttpPost("obterClientes")]
        [Authorize(Roles ="Funcionario,Adm,Gerente")]
        public async Task<IActionResult> ObterClientes([FromBody] PaginacaoTabelaResult<ClienteDTO, FiltroCliente> clienteDTO)
        {

            var ret = await _service.BuscarClientes(clienteDTO);
            if (!ret.success)
            {
                return BadRequest(ret);
            }
            return Ok(ret);
        }
        [HttpPost]
        [Authorize(Roles ="Funcionario,Adm,Gerente")]
        public async Task<IActionResult> Cadastrar(ClienteDTO cliente)
        {


                if (!ModelState.IsValid)
                {
                    var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                    return BadRequest(Response<ClienteDTO>.Failed(menssagemErro));
                }


            var response = await _service.CadastrarCliente(cliente);
            if (response.success)
                return Ok(response);

            return BadRequest(response);

        }
        [HttpDelete]
        [Authorize(Roles = "Funcionario,Adm,Gerente")]
        public async Task<IActionResult> InativarCliente(int id)
        {
            var ret = await _service.InativarCliente(id);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }
        [HttpPut]
        [Authorize(Roles = "Funcionario,Adm,Gerente")]
        public async Task<IActionResult> UpdateCliente(ClienteDTO cliente)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<ClienteDTO>.Failed(menssagemErro));
            }
            var ret = await _service.UpdateCliente(cliente);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }

    }
}
