using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using SistemaEstoque.API.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SistemaEstoque.API.Repository.Interfaces;
using SistemaEstoque.API.Enums;
using Microsoft.OpenApi.Extensions;
using SistemaEstoque.API.DTOs.CadastrosDTOs;

namespace SistemaEstoque.API.Services
{
    public class LoginService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbMethods<LoginModel> _repository;
        public LoginService(IConfiguration configuration, IDbMethods<LoginModel> repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
        public async Task<Response<string>> CriarLogin(LoginDTO login, UsuarioDTO usuario)
        {
            LoginModel model = login;
            model.usuarioId = usuario.id;

            var hashSalt = CriptografiaSenha(login.Password);
            model.Salt = hashSalt["salt"];
            model.Hash = hashSalt["hash"];
            if (await _repository.Create(model))
            {
                var token = await GerarCookie(model);
                return token;
            }
            return Response<string>.Failed("Erro ao salvar o login");
        }
        public async Task<Response<string>> LoginAutenticacao(LoginDTO login)
        {
            try
            {
                LoginModel usuarioAutenticar = await _repository.BuscaDireto(login);
                if (usuarioAutenticar == null)
                    return Response<string>.Failed("Falha ao encontrar o email cadastrado");
                if (!await ValidaSenha(usuarioAutenticar, login.Password))
                {
                    return Response<string>.Failed("Senha incorreta");
                }
                return await GerarCookie(usuarioAutenticar);

            }
            catch (Exception ex)
            {
                return Response<string>.Failed(ex.Message);
            }
        }

        #region Metodos Privados
        private async Task<bool> ValidaSenha(LoginModel usuario, string password)
        {
            try
            {
                using (var hmac = new HMACSHA512(usuario.Salt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    for (var i = 0; i < computedHash.Length; i++)
                    {
                        if (computedHash[i] != usuario.Hash[i])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private Dictionary<string, byte[]> CriptografiaSenha(string password)
        {
            Dictionary<string, byte[]> ret = new Dictionary<string, byte[]>();
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    ret.Add("hash", hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                    ret.Add("salt", hmac.Key);
                }
                return ret;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Falha ao criptografar a senha: " + ex.Message);
            }
        }
        private async Task<Response<string>> GerarCookie(LoginModel login)
        {
            #region Claims
            Claim[] claims = [];
            string tipoUsuario = Enum.GetName(login.TipoUsuario);
            claims = new[]
                  {
                        new Claim("id",login.usuarioId.ToString()),
                        new Claim("user", login.Usuario.Nome.ToString()),
                        new Claim("tipo", Enum.GetName(login.TipoUsuario)),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

            #endregion

            #region GeraToken
            try
            {
                var privateKy = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]));

                var crendentials = new SigningCredentials(privateKy, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(1);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["jwt:issuer"],
                    audience: _configuration["jwt:audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: crendentials);
                return Response<string>.Ok(new JwtSecurityTokenHandler().WriteToken(token));

            }
            catch (Exception ex)
            {
                return Response<string>.Failed(ex.Message);
            }
            #endregion

        }
        #endregion

    }
}
