using SistemaEstoque.API.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.Models
{
    public class ClienteModel
    {
        [Key]
        public int id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public CidadeModel Cidade { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        [DataType(DataType.Date)]
        public DateTime dthNascimeto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public List<PedidoModel> Pedidos { get; set; }
        public EmpresaModel Empresa { get; set; }
        public int EmpresaId { get; set; }
        public short fAtivo { get; set; }

        public static implicit operator ClienteModel(ClienteDTO dto)
        {
            return new ClienteModel
            {
                id = dto.id,
                Bairro = dto.Bairro,
                Cep = dto.CEP,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Cidade = dto.Cidade,
                Documento = dto.Documento,
                dthNascimeto = dto.dthNascimeto,
                EmpresaId = dto.EmpresaId,
                fAtivo = dto.fAtivo,
                Nome = dto.Nome,
                //Pedidos = dto.Pedidos,
                Rua = dto.Rua,
            };
        }
    }
}
