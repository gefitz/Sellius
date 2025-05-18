using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaEstoque.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginService _service;
        public LoginController(LoginService service)
        {
            _service = service;
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginDTO usuario)
        //{
        //    if (usuario.Email == null || usuario.Password == null)
        //    {
        //        return BadRequest();
        //    }
        //    var response = await _service.LoginAutenticacao(usuario);
        //    if (!response.success) {
        //        return BadRequest(response) ; 
        //    }
        //    return Ok(response);
        //}
    }
}
