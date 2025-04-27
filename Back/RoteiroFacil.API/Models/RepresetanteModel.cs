namespace RoteiroFacil.API.Models
{
    public class RepresetanteModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public float TotalVendido { get; set; }
        public float MetaVendas { get; set; }
        public int qtdVendida { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public List<RoteiroModel> Roteiros { get; set; }
        public List<ClienteModel> Clientes { get; set;}

    }
}
