namespace RoteiroFacil.API.Models
{
    public class RoteiroModel
    {
        public int id { get; set; }
        public int CidadeId { get; set; }
        public CidadeModel Cidade { get; set; }
        public DateTime dthRoteiro { get; set; }
        public List<ClienteModel> Cliente { get; set; }

        public int RepresetanteId { get; set; }
        public RepresetanteModel Represetante { get; set; }
        public short fAtivo { get; set; }
    }
}