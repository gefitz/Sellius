using SistemaEstoque.API.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.API.DTOs
{
    public class TipoProdutoDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Necessario o Tipo do Produto")]
        [Display(Name ="Tipo de Produto")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Necessario a Descrição do Tipo")]
        [Display(Name = "Descrição do Tipo")]
        public string Descricao { get; set; }
        public short fAtivo { get; set; }

        public static implicit operator TipoProdutoDTO(TipoProdutoModel model)
        {
            return new TipoProdutoDTO
            {
                id = model.id,
                Tipo = model.Tipo,
                Descricao = model.Descricao,
                fAtivo = model.fAtivo
            };
        }
    }
}