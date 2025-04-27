using RoteiroFacil.API.Context;
using RoteiroFacil.API.Models;

namespace RoteiroFacil.API.Repository.Geral
{
    public class LogRepository
    {
        private readonly RoteiroFacilContext _context;

        public LogRepository(RoteiroFacilContext context)
        {
            _context = context;
        }

        public async void GravarLogBanco(LogModel log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
