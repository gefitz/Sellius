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

namespace SistemaEstoque.API.Services
{
    public class LoginService
    {
        private readonly IConfiguration _configuration;
        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> ValidaSenha(UsuarioModel usuario, string password)
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
        public Dictionary<string, byte[]> CriptografiaSenha(string password)
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
                throw new ApplicationException("Falha ao criptografar a senha: "+ ex.Message);
            }
        }
        public async Task<Response<string>> GerarCookie(UsuarioModel usuario)
        {
            #region GeraToken
            try
            {

                var claims = new[]
                       {
                        new Claim("id",usuario.id.ToString()),
                        new Claim("user", usuario.Nome.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
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
    }
}
