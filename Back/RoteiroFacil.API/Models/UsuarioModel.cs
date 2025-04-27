namespace RoteiroFacil.API.Models
{
    public class UsuarioModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salta { get; set; }
        public string Telefone { get; set; }
        //public InfoBancoModel InfoBanco { get; set; }
        public DateTime dthCriacaoConta { get; set; }
        public LicencaModel Licenca { get; set; }      
        public RepresetanteModel Represetante { get; set; }

    }
}
