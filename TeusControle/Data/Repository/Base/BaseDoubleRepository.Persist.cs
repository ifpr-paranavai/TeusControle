using Core.Domain.Base;
using Data.Context;
using Manager.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository.Base
{
    /// <summary>
    /// Classe genérica para repositório. CRUD genério.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class BaseDoubleRepository<TEntity> : IBaseDoubleRepository<TEntity> where TEntity : BaseDoubleEntity
    {
        protected readonly MyContext _context;
        public BaseDoubleRepository(MyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Insere um novo registro
        /// </summary>
        /// <param name="obj"></param>
        public async Task InsertAsync(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Insere novos registros
        /// </summary>
        /// <param name="obj"></param>
        public async Task InsertAsync(ICollection<TEntity> obj)
        {
            await _context.Set<TEntity>().AddRangeAsync(obj);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <param name="obj"></param>
        public async Task UpdateAsync(TEntity obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deleta um registro a partir do Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        public async Task PhysicalDeleteAsync(
            int id,
            int id2
        )
        {
            _context.Set<TEntity>().Remove(await SelectAsync(id, id2));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualizar alguns campos
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="includeProperties"></param>
        public async Task UpdateFieldsAsync(TEntity entity, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var dbEntry = _context.Entry(entity);

            foreach (var includeProperty in includeProperties)
            {
                dbEntry.Property(includeProperty).IsModified = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}