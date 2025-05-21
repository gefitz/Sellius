using SistemaEstoque.API.DTOs.CadastrosDTOs;
using SistemaEstoque.API.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.Models
{
    public class UsuarioModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public int CidadeId { get; set; }
        public CidadeModel Cidade { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }

        [DataType(DataType.Date)]
        public DateTime dthCadastro { get; set; }
        public int EmpresaId { get; set; }
        public EmpresaModel Empresa { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public static implicit operator UsuarioModel(UsuarioDTO dto)
        {
            return new UsuarioModel
            {
                id = dto.id,
                Nome = dto.Nome,
                Documento = dto.Documento,
                Email = dto.Email,
                Rua = dto.Rua,
                CidadeId =  dto.CidadeId,
                CEP = dto.CEP,
                dthCadastro = dto.dthCadastro,  
                EmpresaId = dto.EmpresaId,
                TipoUsuario = dto.TipoUsuario,
            };
        }
    }
}
