using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Repository.Pedidos.Interfaces
{
    public interface IPedidoRepository:
        IDbMethods<PedidoModel>,
        IPaginacao<PedidoModel,PedidoModel>
        
    {
    }
}
