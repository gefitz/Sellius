using RoteiroFacil.API.Models;

namespace RoteiroFacil.API.DTOs
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telefone { get; set; }
        //public InfoBancoDTO InfoBanco { get; set; }
        public DateTime dthCriacaoConta { get; set; }
    }
}
