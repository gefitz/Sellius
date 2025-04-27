using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoteiroFacil.API.DTOs;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Services.Usuario;

namespace RoteiroFacil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioServices _services;
        private readonly LoginService _loginService;
        private readonly LogModel _log;

        public UsuarioController(UsuarioServices services, LoginService loginService, LogModel log)
        {
            _services = services;
            _loginService = loginService;
            _log = log;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var token = await _loginService.Authentication(login);
            if (string.IsNullOrEmpty(token))
                return BadRequest(_log);
            return Ok(token);

        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario(UsuarioDTO usuario)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (await _services.CreateUsuario(usuario)) { return Ok(); }
            return BadRequest(_log);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(UsuarioDTO usuario)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (await _services.UpdateUsuario(usuario)) { return Ok(); }
            return BadRequest(_log);
        }
    }
}
