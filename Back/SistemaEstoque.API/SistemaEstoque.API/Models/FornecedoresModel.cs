namespace SistemaEstoque.API.Models
{
    public class FornecedoresModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public short  fAtivo { get; set; }
        public DateTime dthCadastro { get; set; }
        public DateTime dthAlteracao { get; set; }
        public EmpresaModel Empresa { get; set; }
        public int EmpresaId { get; set; }

    }
}
