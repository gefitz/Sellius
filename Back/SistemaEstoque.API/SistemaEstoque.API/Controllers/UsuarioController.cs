using SistemaEstoque.API.Models;
using SistemaEstoque.API.Services;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.API.DTOs.CadastrosDTOs;

namespace SistemaEstoque.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuarioController:Controller
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CadastroUsuario(UsuarioDTO usuarioDTO)
        {
            if(!ModelState.IsValid) {
                    return
                    BadRequest("Necessario informalções do usuario"); }
            var response = await _service.CriarUsuario(usuarioDTO);
            if(response.success)
            {
                return Ok(response); 
            }
            return BadRequest(response);

        }
    }
}
