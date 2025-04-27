namespace RoteiroFacil.API.Models
{
    public class CidadeModel
    {
        public int id { get; set; }
        public string Cidade { get; set; }
        public int EstadoId { get; set; }
        public EstadoModel Estado { get; set; }
    }
}