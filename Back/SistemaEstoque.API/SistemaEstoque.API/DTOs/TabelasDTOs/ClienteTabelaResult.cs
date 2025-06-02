using SistemaEstoque.API.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.DTOs.TabelasDTOs
{
    public class ClienteTabelaResult
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string CidadeEstado { get; set; }
        public string Rua { get; set; }
        public string Telefone { get; set; }
        public short fAtivo { get; set; }

        public static implicit operator ClienteTabelaResult(ClienteModel model)
        {
            return new ClienteTabelaResult
            {
                id = model.id,
                Nome = model.Nome,
                Documento = model.Documento,
                Rua = model.Rua,
                Telefone = model.Telefone,
                fAtivo = model.fAtivo,
                CidadeEstado = model.Cidade.Cidade + " / " + model.Cidade.Estado.Sigla
            };
        }
    }
}
