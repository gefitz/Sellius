using SistemaEstoque.API.Enums;

namespace SistemaEstoque.API.Models
{
    public class LicencaModel
    {
        public int id { get; set; }
        public Guid Licenca { get; set; }
        public DateTime dthVencimento { get; set; }
        public DateTime dthInicioLincenca { get; set; }
        public decimal ValorMensal { get; set; }
        public int UsuariosIncluidos { get; set; }
        public int ValorPorUsuario { get; set; }
        public TipoLicenca TipoLincenca { get; set; }
    }
}