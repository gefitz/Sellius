using Microsoft.IdentityModel.Tokens;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Services.Geral;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoteiroFacil.API.Services.Usuario
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly LogService _log;

        public TokenService(IConfiguration configuration, LogService log)
        {
            _configuration = configuration;
            _log = log;
        }

        public string GenerateToken(UsuarioModel usuario)
        {
            try
            {

                var claims = new[]
                       {
                        new Claim("id",usuario.id.ToString()),
                        new Claim("user", usuario.Email.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                var privateKy = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]));

                var crendentials = new SigningCredentials(privateKy, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddMinutes(10);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["jwt:issuer"],
                    audience: _configuration["jwt:audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: crendentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _log.RegistrarInnerException(ex, "TokenService");
                return null;
            }
        }
    }
}
