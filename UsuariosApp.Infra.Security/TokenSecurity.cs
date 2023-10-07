using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Security;

namespace UsuariosApp.Infra.Security
{
    public class TokenSecurity : ITokenSecurity
    {
        //chave secreta antifalsificação
        public static string SecretKey => "978A023502D94A9CB785A7307A45E94A";

        public string CreateToken(Usuario usuario)
        {
            //informações do usuário
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, "USER")
            };

            //gerando a chave antifalsificação do token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //preenchendo as informações do token
            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims, //informações de identificação do usuário
                expires: DateTime.Now.AddHours(2), //tempo de expiração do token
                signingCredentials: credentials //assinatura do token
                );

            //retornando o token
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}