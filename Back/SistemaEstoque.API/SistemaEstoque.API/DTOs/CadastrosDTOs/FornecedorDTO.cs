using SistemaEstoque.API.Models;

namespace SistemaEstoque.API.DTOs.CadastrosDTOs
{
    public class FornecedorDTO
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public short fAtivo { get; set; }
        public DateTime dthCadastro { get; set; }
        public DateTime dthAlteracao { get; set; }
        public int EmpresaId { get; set; }
        public int CidadeId { get; set; }

        public static implicit operator FornecedorDTO(FornecedoresModel model)
        {
            return new FornecedorDTO
            {
                id = model.id,
                Nome = model.Nome,
                CNPJ = model.CNPJ,
                Telefone = model.Telefone,
                Email = model.Email,
                fAtivo = model.fAtivo,
                dthAlteracao = model.dthAlteracao,
                dthCadastro = model.dthCadastro,
                EmpresaId = model.EmpresaId
            };
        }
        public static List<FornecedorDTO> FromList(List<FornecedoresModel> list)
        {
            List<FornecedorDTO> dtp = new List<FornecedorDTO>();
            for (int i = 0; i < list.Count; i++)
            {
                dtp.Add(list[i]);
            }
            return dtp;
        }
    }
}
