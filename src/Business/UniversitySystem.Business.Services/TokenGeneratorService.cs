namespace UniversitySystem.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using Contracts;
    using Common;

    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration configuration;

        public TokenGeneratorService(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[GlobalJWTConstants.Key]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration[GlobalJWTConstants.Issuer],
              configuration[GlobalJWTConstants.Issuer],
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
