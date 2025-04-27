namespace RoteiroFacil.API.Models
{
    public class InfoBancoModel
    {
        public int id { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }

    }
}