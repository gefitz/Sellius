using SistemaEstoque.API.Enums;
using SistemaEstoque.API.Models;

namespace SistemaEstoque.API.DTOs.CadastrosDTOs
{
    public class EmpresaDTO
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public CidadeDTO Cidade { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public DateTime dthCadastro { get; set; }
        public DateTime dthAlteracao { get; set; }
        public TipoLicenca TipoLicenca { get; set; }

        public static implicit operator  EmpresaDTO(EmpresaModel dto)
        {
            return new EmpresaDTO
            {
                id = dto.id,
                Nome = dto.Nome,
                CNPJ = dto.CNPJ,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Rua = dto.Rua,
                Cidade = dto.Cidade,
                CEP = dto.CEP,
                dthAlteracao = dto.dthAlteracao,
                dthCadastro = dto.dthCadastro,
            };
        }
    }
}
