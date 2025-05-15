namespace SistemaEstoque.API.Models
{
    public class EstadoModel
    {
        public int id { get; set; }
        public string Estado { get; set; }
        public string Sigla { get; set; }
        public List<CidadeModel> Cidade { get; set; }
    }
}