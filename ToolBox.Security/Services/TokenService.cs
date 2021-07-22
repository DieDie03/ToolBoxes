using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Security.Configuration;
using ToolBox.Security.Configuration.Interfaces;

namespace ToolBox.Security.Services
{
    public class TokenService
    {
        private readonly JwtSecurityTokenHandler _handler;

        private readonly Config _config;
        private readonly SymmetricSecurityKey _securityKey;

        public TokenService(Config configuration)
        {
            //Raccourci pour new JwtSecurityTokenHandler();
            _handler = new();
            _config = configuration;
            _securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config.Signature));
        }
        public string CreateToken(IPayload payload)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                GetClaims(payload),//Liste des Claims
                DateTime.Now,
                _config.ValidateDate ? DateTime.Now.AddSeconds(_config.Duration) : null,
                new SigningCredentials(_securityKey
                 , SecurityAlgorithms.HmacSha512) //Signature
            );
            return _handler.WriteToken(token);
        }

        private IEnumerable<Claim> GetClaims(IPayload payload)
        {
            yield return new Claim(ClaimTypes.Email, payload.Email);
            yield return new Claim(ClaimTypes.NameIdentifier, payload.Identifier);
            yield return new Claim(ClaimTypes.Role, payload.Role);
            Type payloadType = payload.GetType();
            PropertyInfo[] properties = payloadType.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                yield return new Claim(propertyInfo.Name, propertyInfo.GetValue(payload).ToString());
            }
        }

        public ClaimsPrincipal DecodeToken(string token)
        {
            return null;
        }
    }
}
