using AutoMapper;
using Core.Domain;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Core.Shared.Models.User;
using Manager.Implementation.Base;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Manager.Interfaces.Services;
using Manager.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public partial class UserManager : BaseManager<User>, IUserManager
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;
        private readonly ILogger<UserManager> logger;

        public UserManager(
            IHttpContextAccessor httpContextAccessor, 
            IUserRepository usersRepository, 
            IMapper mapper, 
            IJwtService jwtService,
            ILogger<UserManager> logger
        ) : base (usersRepository, httpContextAccessor, mapper)
        {
            this.userRepository = usersRepository;
            this.mapper = mapper;
            this.jwtService = jwtService;
            this.logger = logger;
        }

        public async Task<UserModel> Insert(CreateUserModel newUser)
        {
            try
            {
                User user = mapper.Map<User>(newUser);
                ConvertPasswordToHash(user);

                var data = await AddAsync<UserModel>(user);
                data.Password = "";
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO INSERIR USUÁRIO: {@newUser}", newUser, ex);
            }
            return null;
        }

        public async Task<UserModel> Update(UpdateUserModel updatedUser)
        {
            try
            {
                var user = mapper.Map<User>(updatedUser);
                ConvertPasswordToHash(user);

                var data = await UpdateAsync<UserModel>(user);
                data.Password = "";
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO ATUALIZAR USUÁRIO: {@newUser}", updatedUser, ex);
            }
            return null;
        }

        public async Task<UserModel> DeleteById(int id)
        {
            try
            {
                var user = await LogicalDeleteAsync<UserModel>(id);
                user.Password = "";
                return user;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO EXCLUIR USUÁRIO COM ID: {@id}", id, ex);
                return null;
            }
        }

        public virtual async Task<object> GetById(int id)
        {
            try
            {
                if (!await AnyAsync(x =>
                    x.Id == id &&
                    !x.Deleted
                ))
                    throw new Exception("Registro não encontrado.");

                var data = Query(x => x.Id == id)
                    .Select(s => new 
                    {
                        Id = s.Id,
                        Name = s.Name,
                        /*CpfCnpj = s.CpfCnpj,
                        DocumentType = s.DocumentType,*/
                        ProfileImage = s.ProfileImage,
                        ProfileTypeDescription = EnumExtension.GetDescription(s.ProfileType), // TODO: listar descrição e editar model cadastro e edição
                        ProfileType = s.ProfileType,
                        Email = s.Email,
                        Active = s.Active,
                        CreatedDate = s.CreatedDate != null ? ((DateTime)s.CreatedDate).ToString("dd/MM/yyyy HH:mm:ss") : "",
                        LastChange = s.LastChange != null ? ((DateTime)s.LastChange).ToString("dd/MM/yyyy HH:mm:ss") : "",
                        BirthDate = (DateTime)s.BirthDate,
                    })
                    .FirstOrDefault();

                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO BUSCAR USUÁRIO COM ID: {@id}", id, ex);
            }
            return null;
        }

        public new PaginatedResponse<UserPagedModel> GetPaged(PaginatedRequest pagingParams)
        {
            try
            {
                var paginatedUsers = GetPaged(
                    pagingParams,
                    x => new UserPagedModel {
                        Id = x.Id,
                        Name = x.Name,
                        ProfileType = EnumExtension.GetDescription(x.ProfileType),
                        /*CpfCnpj = x.CpfCnpj,*/
                        Email = x.Email,
                        BirthDate = x.BirthDate.Value.ToString("dd/MM/yyyy"),
                        CanBeDeleted = x.Id != GetLoggedUserId()
                    }
                );

                return paginatedUsers;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO REALIZAR BUSCA PÁGINADA DE USUÁRIOS COM OS PARAMETROS: {@pagingParams}", pagingParams, ex);
            }
            return null;
        }
    }
}
