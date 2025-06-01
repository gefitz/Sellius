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


        //Tentar implementar futuramente um fromList dinamico na paginação assim matando o from list em cada dtos
        //public static List<model> fromList(List<model> lista)
        //{
        //    List<model> ret = new List<model>();

        //    for (int i = 0; i < lista.Count; i++)
        //    {
        //        ret.Add(lista[i]);
        //    }
        //    return ret;

        //}

    }
}
