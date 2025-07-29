using SistemaEstoque.API.Models.Cliente;
using SistemaEstoque.API.Repository.Interfaces;
using System.Data;

namespace SistemaEstoque.API.Repository.Cliente.Interfaces
{
    public interface ISegmentacaoRepository : IDbMethods<SegmentacaoModel>, IPaginacao<SegmentacaoModel,SegmentacaoModel>
    {
        Task<List<SegmentacaoModel>>CarregarCombo(int idEmpresa);
    }
}
