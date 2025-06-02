using SistemaEstoque.API.Enums;
using SistemaEstoque.API.Models;

namespace SistemaEstoque.API.DTOs.CadastrosDTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ClienteId { get; set; }
        public int? usuarioId { get; set; }
        public int EmpresaId { get; set; }

    }
}
