﻿using Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager.Interfaces.Repositories.Base
{
    /// <summary>
    /// Interface genérica para repositório. Declaração dos métodos para CRUD.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Insere um novo registro
        /// </summary>
        /// <param name="obj"></param>
        Task InsertAsync(TEntity obj);

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <param name="obj"></param>
        Task UpdateAsync(TEntity obj);

        /// <summary>
        /// Deleta um registro a partir do Id
        /// </summary>
        /// <param name="id"></param>
        Task PhysicalDeleteAsync(int id);

        /// <summary>
        /// Atualizar alguns campos
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="includeProperties"></param>
        Task UpdateFieldsAsync(
            TEntity entity,
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        /// <summary>
        /// Busca todos os registros
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntity>> SelectAsync();

        /// <summary>
        /// Busca um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> SelectAsync(int id);

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
        /// Busca quantidade de registros a partir do filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Busca páginada
        /// </summary>
        /// <param name="initialRow"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetPagedAsync(
            int initialRow,
            int pageSize,
            Expression<Func<TEntity, bool>> filter
        );
    }
}