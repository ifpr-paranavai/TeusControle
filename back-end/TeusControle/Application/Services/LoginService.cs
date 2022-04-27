using EnumsNET;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Domain.Models.Enums;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Services
{
    public class LoginService : ILoginService
    {
        private IConfiguration _config;
        private IUsersService _usersService;
        public LoginService(
            IConfiguration configuration,
            IUsersService usersService
        )
        {
            _config = configuration;
            _usersService = usersService;
        }

        /// <summary>
        /// Gera token de acesso
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        public ResponseMessages<string> GenerateToken(TokenLogin credential)
        {
            var validUser = ValidateUser(credential);
            if (!validUser.Sucess)
                return new ResponseMessages<string>(
                    status: false,
                    message: "Usuário não encontrado!"
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(CustomClaimTypes.Id, validUser.Data.Id.ToString()),
                    new Claim(CustomClaimTypes.Name, validUser.Data.Name.ToString()),
                    new Claim(CustomClaimTypes.Email, validUser.Data.Email.ToString()),
                    new Claim(CustomClaimTypes.ProfileImage, validUser.Data.ProfileImage != null ? validUser.Data.ProfileImage.ToString() : ""),
                    new Claim(CustomClaimTypes.ProfileTypeId, validUser.Data.ProfileTypeId.ToString()),
                    new Claim(CustomClaimTypes.ProfileTypeDescription, validUser.Data.ProfileTypeDescription.ToString()),// alterar para descrição do enumerador
                    new Claim(CustomClaimTypes.UserName,validUser.Data.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ResponseMessages<string>(
                status: true,
                message: "Login efetuado com sucesso!",
                data: tokenHandler.WriteToken(token)
            );
        }

        /// <summary>
        /// Usuário existe? Se sim, retorna usuário
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        private ReturnData<UserDataToken> ValidateUser(TokenLogin credential)
        {
            Expression<Func<Users, bool>> filter = x =>
                x.UserName == credential.UserName &&
                x.Password == credential.Password &&
                !x.Deleted &&
                x.Active;

            if (!_usersService.Any(filter))
                return new ReturnData<UserDataToken>(sucess: false);

            var user = _usersService.Query(filter)
                .Select(s => new UserDataToken
                {
                    Id = s.Id,
                    Name = s.Name,
                    UserName = s.UserName,
                    Email = s.Email,
                    ProfileImage = s.ProfileImage,
                    ProfileTypeId = (int)s.ProfileType,
                    ProfileTypeDescription = ((ProfileTypesEnum)s.ProfileType).AsString(EnumFormat.Description)
                })
                .FirstOrDefault();

            return new ReturnData<UserDataToken>(
                sucess: true,
                data: user
            );
        }
    }

    public class UserDataToken
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Imagem de perfil
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Id do tipo do perfil
        /// </summary>
        public int ProfileTypeId { get; set; }

        /// <summary>
        /// Descrição do tipo do perfil
        /// </summary>
        public string ProfileTypeDescription { get; set; }
    }

    public static class CustomClaimTypes
    {
        public const string Id = "id";

        public const string Name = "name";

        public const string Email = "email";
        
        public const string ProfileImage = "profileimage"; 
        
        public const string ProfileTypeId = "profiletypeid";
        
        public const string ProfileTypeDescription = "profileTypeDescription"; 
        
        public const string UserName = "username";
    }
}
