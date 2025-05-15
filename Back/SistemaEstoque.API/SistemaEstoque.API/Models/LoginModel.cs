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
        public bool fEmailConfirmado { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public int? usuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }
        public int? clienteId { get; set; }
        public ClienteModel? Cliente { get; set; }
    }
}
