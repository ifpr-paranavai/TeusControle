using Core.Domain.Base;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager.Interfaces.Managers.Base
{
    /// <summary>
    /// Interface da classe genérica dos serviços. Declaração dos métodos para CRUD.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseManager<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Cria um novo registro
        /// </summary>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task<TOutputModel> AddAsync<TOutputModel>(TEntity inputModel)
            where TOutputModel : class;

        /// <summary>
        /// Exclui fisicamente um registro a partir do id
        /// </summary>
        /// <param name="id"></param>
        Task PhysicalDeleteAsync(int id);

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <typeparam name="TInputModel"></typeparam>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        Task<TOutputModel> UpdateAsync<TInputModel, TOutputModel>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class;

        /// <summary>
        /// Busca quantidade de registros a partir do filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Retorna se para a condição, existe tal registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Constrói busca no banco
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Atualiza alguns campos
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedProperties"></param>
        Task UpdateSomeFieldsAsync(
            TEntity entity,
            params Expression<Func<TEntity, object>>[] updatedProperties
        );

        /// <summary>
        /// Busca páginada
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<PageResponse> GetPagedAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>> filter
        );

        /// <summary>
        /// Busca páginada com parâmetros
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<PaginatedResponse<Object>> GetPagedAsync(
            PaginatedRequest pagingParams, 
            Func<TEntity, Object> selector
        );
        
        /// <summary>
        /// Exclusão lógica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task LogicalDeleteAsync(int id);

        /// <summary>
        /// Busca páginada com parâmetros
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        Task<PaginatedResponse<TEntity>> GetPagedAsync(PaginatedRequest pagingParams);

        /* /// <summary>
         /// Busca páginada com parâmetros
         /// </summary>
         /// <param name="pagingParams"></param>
         /// <returns></returns>
         Task<PaginatedResponse<TEntity>> GetPaged(PaginatedRequest pagingParams);

         /// <summary>
         /// Busca páginada com parâmetros
         /// </summary>
         /// <param name="pagingParams"></param>
         /// <param name="selector"></param>
         /// <returns></returns>
         Task<PaginatedResponse<Object>> GetPaged(PaginatedRequest pagingParams, Func<TEntity, Object> selector);*/
    }
}