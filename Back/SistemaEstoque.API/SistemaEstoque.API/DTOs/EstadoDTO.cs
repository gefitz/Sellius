using SistemaEstoque.API.Models;

namespace SistemaEstoque.API.DTOs
{
    public class EstadoDTO
    {
        public int id { get; set; }
        public string Estado { get; set; }
        public string Sigla { get; set; }

        public static implicit operator EstadoDTO(EstadoModel dto)
        {
            return new EstadoDTO { id = dto.id, Sigla = dto.Sigla, Estado = dto.Estado };
        }
    }
}
