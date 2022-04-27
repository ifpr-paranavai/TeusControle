using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TeusControle.Application.Interfaces.Repository;
using TeusControle.Domain.Models.CommonModels;

namespace TeusControle.Infrastructure.Repository
{
    /// <summary>
    /// Classe genérica para repositório. CRUD genério.
    /// </summary>
    public partial class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Busca todos os registros
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> Select() =>
            _context.Set<TEntity>().ToList();

        /// <summary>
        /// Busca um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Select(long id) =>
            _context.Set<TEntity>().Find(id);

        /// <summary>
        /// Retorna se para a condição, existe tal registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            return query.Any(filter);
        }

        /// <summary>
        /// Constrói busca no banco
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter == null)
                throw new ArgumentNullException("É necessário informar o filtro");

            return query.Where(filter).AsNoTracking();
        }

        /// <summary>
        /// Busca quantidade de registros a partir do filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = query.Where(filter);

            return query.Count();
        }

        /// <summary>
        /// Busca página
        /// </summary>
        /// <param name="initialRow"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPaged(
            int initialRow,
            int pageSize,
            Expression<Func<TEntity, bool>> filter
        )
        {
            return Query(filter)
                .Skip(initialRow)
                .Take(pageSize)
                .ToList();
        }
    }
}