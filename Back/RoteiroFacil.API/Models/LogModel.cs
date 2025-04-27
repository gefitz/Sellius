namespace RoteiroFacil.API.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime dthErro { get; set; }
        public string ClassErro { get; set; }
        public string? InnerException { get; set; }

    }
}
