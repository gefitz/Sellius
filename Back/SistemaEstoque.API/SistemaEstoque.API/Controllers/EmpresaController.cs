using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Services;

namespace SistemaEstoque.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmpresaController : Controller
    {
        private EmpresaService _service;
        private LoginService _loginService;
        private UsuarioService _usarioService;
        public EmpresaController(EmpresaService service,LoginService loginService, UsuarioService usuarioService)
        {
            _service = service;
            _loginService = loginService;
            _usarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarEmpresaAsync(CadastroNovoEmpresaDTO nova)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Response<EmpresaDTO>.Failed("Faltando informações na DTOs"));
            }
            var response = await _service.CadastrarNovaEmpresa(nova);

            if (!response.success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
