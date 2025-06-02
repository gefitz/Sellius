using SistemaEstoque.API.DTOs.TabelasDTOs;

namespace SistemaEstoque.API.Repository.Interfaces
{
    public interface IPaginacao<model,filtro>
    {
        public Task<PaginacaoTabelaResult<model, filtro>> Filtrar(PaginacaoTabelaResult<model, filtro> obj);

    }
}
