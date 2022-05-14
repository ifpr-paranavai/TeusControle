using Core.Domain;
using Core.Shared.Models;
using Manager.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value);
            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Id, user.Id.ToString()),
                new Claim(CustomClaimTypes.Name, user.Name.ToString()),
                new Claim(CustomClaimTypes.Email, user.Email.ToString()),
                new Claim(CustomClaimTypes.ProfileImage, user.ProfileImage != null ? user.ProfileImage.ToString() : ""),
                new Claim(CustomClaimTypes.ProfileTypeId, user.ProfileType.ToString()),
                /*new Claim(CustomClaimTypes.ProfileTypeDescription, user.ProfileTypeDescription.ToString()),// alterar para descrição do enumerador*/
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = configuration.GetSection("JWT:Audience").Value,
                Issuer = configuration.GetSection("JWT:Issuer").Value,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration.GetSection("JWT:ExpiresInMinutes").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
