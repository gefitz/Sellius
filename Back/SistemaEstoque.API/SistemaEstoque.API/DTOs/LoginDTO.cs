using SistemaEstoque.API.Enums;
using SistemaEstoque.API.Models;

namespace SistemaEstoque.API.DTOs
{
    public class LoginDTO
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Documento { get; set; }
        public bool fEmailConfirmado { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
