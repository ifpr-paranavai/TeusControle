﻿using AutoMapper;
using Core.Domain;
using Core.Shared.Models.Request;
using Core.Shared.Models.User;
using Manager.Implementation.Base;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Manager.Interfaces.Services;
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
                data.Password = ""; // TODO: Remover senha do objeto de retorno
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
                data.Password = ""; // TODO: Remover senha do objeto de retorno
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO ATUALIZAR USUÁRIO: {@newUser}", updatedUser, ex);
            }
            return null;
        }

        public async Task DeleteById(int id)
        {
            try
            {
                await LogicalDeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO EXCLUIR USUÁRIO COM ID: {@id}", id, ex);
            }
        }

        public async Task<object> GetById(long id)
        {
            try
            {
                if (!await AnyAsync(x =>
                    x.Id == id &&
                    !x.Deleted &&
                    x.Active
                ))
                    throw new Exception("Registro não encontrado.");

                var data = Query(x => x.Id == id)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        s.CpfCnpj,
                        s.DocumentType,
                        s.ProfileImage,
                        s.ProfileType,
                        s.Email,
                        s.Active,
                        s.CreatedDate,
                        s.BirthDate
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

        public async Task<object> GetPaged(PaginatedRequest pagingParams)
        {
            try
            {
                var paginatedUsers = await GetPagedAsync(
                    pagingParams,
                    x => new {
                        x.Id,
                        x.Name,
                        x.CpfCnpj,
                        x.Email,
                        BirthDate = x.BirthDate.Value.ToString("dd/MM/yyyy")
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
