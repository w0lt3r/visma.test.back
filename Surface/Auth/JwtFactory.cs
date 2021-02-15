using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class JwtFactory
    {
        private readonly IConfiguration _configuration;
        public JwtFactory(
            IConfiguration configuration
        )
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateJwt(int id, string username, int roleId, string roleName)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("id", id.ToString()),
                new Claim("username", username),
                new Claim("roleId", roleId.ToString()),
                new Claim("roleName", roleName)
            });
            return await buildEncodedToken(claimsIdentity);
        }

        private async Task<string> buildEncodedToken(ClaimsIdentity claimsIdentity)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtIssuerOptions:key").Value));
            var signedKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                claims: claimsIdentity.Claims.ToList(),
                expires: DateTime.UtcNow.AddMinutes(Double.Parse(_configuration.GetSection("JwtIssuerOptions:expirationTime").Value)),
                signingCredentials: signedKey);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
