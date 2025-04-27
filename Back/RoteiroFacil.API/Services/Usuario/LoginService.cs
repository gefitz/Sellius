using RoteiroFacil.API.DTOs;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository;
using RoteiroFacil.API.Repository.Interfaces;
using RoteiroFacil.API.Services.Geral;
using System.Security.Cryptography;
using System.Text;

namespace RoteiroFacil.API.Services.Usuario
{
    public class LoginService
    {
        private readonly IRepositoryCRUD<UsuarioModel> _repository;
        private readonly TokenService _tokenService;
        private readonly LogService _log;
        public LoginService(IRepositoryCRUD<UsuarioModel> repository, TokenService tokenService,LogService log)
        {
            _repository = repository;
            _tokenService = tokenService;
            _log = log;
        }

        public async Task<Dictionary<string, byte[]>> EncryptionPassword(string password)
        {
            Dictionary<string, byte[]> hashSalt = new Dictionary<string, byte[]>();
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    hashSalt.Add("hash", hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                    hashSalt.Add("salt", hmac.Key);
                }
                return hashSalt;
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "LoginService");
                return null;
            }
        }
        public async Task<string> Authentication(LoginDTO login)
        {
            UsuarioModel usuario = new UsuarioModel() { Email = login.Email };
            usuario = (await _repository.SearchObj(usuario)).FirstOrDefault();
            if (usuario == null)
            {
                _log.RegistrarLog("Usuario não foi encontrado", "LoginService", false);
                return null;
            }
            if (ValidPassword(login.Password, usuario))
            {
                return _tokenService.GenerateToken(usuario);
            }
            return null;
        }
        private bool ValidPassword(string password, UsuarioModel usuario)
        {
            try
            {
                using (var hmac = new HMACSHA512(usuario.Salta))
                {
                    var HashLogin = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                    for (int i = 0; i < HashLogin.Length; i++)
                    {
                        if (HashLogin[i] != usuario.Hash[i])
                        {
                            _log.RegistrarLog("Senha incorreta", "LoginService", false);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "LoginService");
                return false;
            }
        }
    }
}
