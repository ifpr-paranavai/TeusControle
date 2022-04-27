﻿using System.Threading.Tasks;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Interfaces.Services
{
    /// <summary>
    /// Inteface para serviço de usuários. Declaração de métodos
    /// </summary>
    public interface IUsersService : IBaseService<Users>
    {
        /// <summary>
        /// Insere um novo usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ResponseMessages<object> Insert(CreateUserModel user);

        /// <summary>
        /// Atualiza um registro de usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ResponseMessages<object> Update(UpdateUserModel user);

        /// <summary>
        /// Busca um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseMessages<object> GetById(long id);

        /// <summary>
        /// Busca lista de todos os usuários
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ResponseMessages<object> Get(
            int pageNumber = 1,
            int pageSize = 10
        );

        /// <summary>
        /// Deleta um registro de usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseMessages<object> DeleteById(long id);

        /// <summary>
        /// Busca lista de todos usuários levando em consideração parametros de filtragem e ordenação
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        Task<ResponseMessages<object>> Get(PaginatedInputModel pagingParams);
    }
}
