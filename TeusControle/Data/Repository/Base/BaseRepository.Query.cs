using Core.Domain.Base;
using Manager.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository.Base
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
        public async Task<IList<TEntity>> SelectAsync() =>
            await _context.Set<TEntity>().ToListAsync();

        /// <summary>
        /// Busca um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> SelectAsync(int id) =>
            await _context.Set<TEntity>().FindAsync(id);

        /// <summary>
        /// Retorna se para a condição, existe tal registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            return await query.AnyAsync(filter);
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
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = query.Where(filter);

            return await query.CountAsync();
        }

        /// <summary>
        /// Busca página
        /// </summary>
        /// <param name="initialRow"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetPagedAsync(
            int initialRow,
            int pageSize,
            Expression<Func<TEntity, bool>> filter
        )
        {
            return await Query(filter)
                .Skip(initialRow)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}