namespace RoteiroFacil.API.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int CidadeId { get; set; }
        public CidadeModel Cidade { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public DateTime dthAlteracao { get; set; }
        public DateTime dthCriacao { get; set; }
        public short fAtivo { get; set; }
        public IEnumerable<PedidosModel> Pedidos { get; set; }
        public int RepresetanteId { get; set; }
        public RepresetanteModel Represetante { get; set; }
        public int? RoteiroId { get; set; }
        public RoteiroModel? Roteiro { get; set; }
    }
}