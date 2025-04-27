using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Geral;

namespace RoteiroFacil.API.Services.Geral
{
    public class LogService
    {
        private readonly LogModel _logModel;
        private readonly LogRepository _repository;

        public LogService(LogModel logModel, LogRepository repository)
        {
            _logModel = logModel;
            _repository = repository;
        }

        private void GravarLogBanco()
        {
            _logModel.dthErro = DateTime.Now;
            _repository.GravarLogBanco(_logModel);
        }
        public void RegistrarLog(string message, string classError, bool gravarBanco)
        {
            _logModel.Message = message;
            _logModel.ClassErro = classError;
            _logModel.InnerException = "";
            if (gravarBanco)
            {
                GravarLogBanco();
            }
        }
        public void RegistrarInnerException(Exception exception, string classError)
        {
            if (exception.InnerException != null)
            {
                _logModel.InnerException = exception.InnerException.ToString();
                _logModel.Message = exception.Message;
                _logModel.ClassErro = classError;
                GravarLogBanco();
            }
        }
    }
}
