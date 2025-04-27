namespace RoteiroFacil.API.Models
{
    public class LicencaModel
    {
        public int id { get; set; }
        public Guid Licenca { get; set; }
        public DateTime dthVencimentoLicenca { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public short fAtivo { get; set; }
    }
}
