namespace SistemaEstoque.API.DTOs.TabelasDTOs
{
    public class PaginacaoTabelaResult<model, filtro>
    {
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public int TamanhoPagina {  get; set; }
        public List<model>? Dados { get; set; }
        public filtro? Filtro { get; set; }
    }
}
