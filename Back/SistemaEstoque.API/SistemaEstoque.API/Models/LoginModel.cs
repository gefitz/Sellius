using SistemaEstoque.API.DTOs.CadastrosDTOs;
using SistemaEstoque.API.Enums;

namespace SistemaEstoque.API.Models
{
    public class LoginModel
    {
        public int id { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string Documento { get; set; }
        public bool fEmailConfirmado { get; set; } = false;
        public TipoUsuario TipoUsuario { get; set; } = 0;
        public int? usuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }

        public static implicit operator LoginModel(LoginDTO dto)
        {
            return new LoginModel
            {
                id = dto.id,
                Email = dto.Email,
                Documento = dto.Documento,
                fEmailConfirmado = dto.fEmailConfirmado,
                TipoUsuario = dto.TipoUsuario,
            };
        }
    }
}
