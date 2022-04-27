using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeusControle.Application.Interfaces.Repository;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Application.Validators;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Services
{
    /// <summary>
    /// Usuários
    /// ToDo: criptografar senhas
    /// </summary>
    public class UsersService : BaseService<Users>, IUsersService
    {
        private IUsersRepository _baseRepository;

        public UsersService(
            IHttpContextAccessor httpContextAccessor,
            IUsersRepository baseRepository, 
            IMapper mapper
        ) : base(
            (IBaseRepository<Users>)baseRepository,
            httpContextAccessor,
            mapper
        )
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Insere um novo usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseMessages<object> Insert(CreateUserModel user)
        {
            try
            {
                var data = Add<CreateUserModel, UserModel, UsersValidator>(user);
                return new ResponseMessages<object>(
                    status: true, 
                    data: data, 
                    message: "Usuário cadastrado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false, 
                    message: $"Ocorreu um erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Atualiza um registro de usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseMessages<object> Update(UpdateUserModel user)
        {
            try
            {
                var data = Update<UpdateUserModel, UserModel, UsersValidator>(user);
                return new ResponseMessages<object>(
                    status: true,
                    message: "Usuário alterado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Ocorreu um erro: { ex.Message }"
                );  
            }
        }

        /// <summary>
        /// Busca registro por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessages<object> GetById(long id)
        {
            try
            {
                if (!_baseRepository.Any(x => 
                    x.Id == id &&
                    !x.Deleted &&
                    x.Active
                ))
                    throw new Exception("Registro não encontrado.");
                
                var data = _baseRepository
                    .Query(x => x.Id == id)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        s.CpfCnpj,
                        s.DocumentType,
                        s.ProfileImage,
                        s.ProfileType,
                        s.Email,
                        s.UserName,
                        s.Active,
                        s.CreatedDate,
                        s.BirthDate
                    })
                    .FirstOrDefault();

                return new ResponseMessages<object>(
                    status: true,
                    message: "Usuário encontrado.",
                    data: data
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }

        /// <summary>
        /// Busca lista de todos os usuários
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseMessages<object> Get(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                var users = GetPaged(
                    pageNumber,
                    pageSize,
                    x => !x.Deleted
                );

                return new ResponseMessages<object>(
                    status: true,
                    message: "Busca realizada com sucesso.",
                    data: users
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }

        /// <summary>
        /// Busca lista de todos usuários levando em consideração parametros de filtragem e ordenação
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        public async Task<ResponseMessages<object>> Get(PaginatedInputModel pagingParams)
        {
            try
            {
                var users = await GetPaged(
                    pagingParams,
                    x => new { 
                        x.Id,
                        x.Name,
                        x.CpfCnpj,
                        x.Email,
                        BirthDate = x.BirthDate.Value.ToString("dd/MM/yyyy")
                    }
                );

                return new ResponseMessages<object>(
                    status: true,
                    message: "Busca realizada com sucesso.",
                    data: users
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }
        /// <summary>
        /// Deleta um registro de usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessages<object> DeleteById(long id)
        {
            try
            {
                LogicalDelete(id);

                return new ResponseMessages<object>(
                    status: true,
                    message: "Usuário deletado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }
    }
}
