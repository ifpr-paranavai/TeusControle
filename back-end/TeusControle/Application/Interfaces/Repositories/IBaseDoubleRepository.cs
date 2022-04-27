using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TeusControle.Domain.Models.CommonModels;

namespace TeusControle.Application.Interfaces.Repository
{
    /// <summary>
    /// Interface genérica para repositório para entidade com chave composta. Declaração dos métodos para CRUD.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseDoubleRepository<TEntity> where TEntity : BaseDoubleEntity
    {
        /// <summary>
        /// Insere um novo registro
        /// </summary>
        /// <param name="obj"></param>
        void Insert(TEntity obj);

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <param name="obj"></param>
        void Update(TEntity obj);

        /// <summary>
        /// Deleta um registro a partir do Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        void PhysicalDelete(
            long id,
            long id2
        );

        /// <summary>
        /// Atualizar alguns campos
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="includeProperties"></param>
        void UpdateFields(
            TEntity entity, 
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        /// <summary>
        /// Busca um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        TEntity Select(
            long id,
            long id2
        );

        /// <summary>
        /// Retorna se para a condição, existe tal registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Constrói busca no banco
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Busca quantidade de registros a partir do filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Busca página
        /// </summary>
        /// <param name="initialRow"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged(
            int initialRow,
            int pageSize,
            Expression<Func<TEntity, bool>> filter
        );
    }
}