using SistemaEstoque.API.Enums;
using SistemaEstoque.API.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.DTOs
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public int CidadeId { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        [DataType(DataType.Date)]
        public DateTime dthCadastro { get; set; }
        public int EmpresaId { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public static implicit operator  UsuarioDTO(UsuarioModel model)
        {
            return new UsuarioDTO
            {
                id = model.id,
                Nome = model.Nome,
                Documento = model.Documento,
                Email = model.Email,
                Rua = model.Rua,
                CidadeId = model.CidadeId,
                CEP = model.CEP,
                dthCadastro = model.dthCadastro,
                EmpresaId = model.EmpresaId,
                TipoUsuario = model.TipoUsuario
            };
        }
    }
}
