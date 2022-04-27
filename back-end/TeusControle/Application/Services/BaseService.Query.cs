using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Domain.Models.CommonModels;
using TeusControle.Infrastructure.Dtos;
using TeusControle.Infrastructure.Queries;

namespace TeusControle.Application.Services
{
    /// <summary>
    /// Classe de serviço genérica. CRUD genérico.
    /// </summary>
    public partial class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity, new()
    {


        /// <summary>
        /// Busca quantidade de registros a partir do filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return _baseRepository.Count(filter);
        }

        /// <summary>
        /// Calcula qual a linha de start da listagem, conforme número da pagina atual e total de paginas
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected int CalcStartRow(
            int pageNumber, 
            int pageSize
        )
        {
            var startRow = (pageNumber - 1) * pageSize;
            if (startRow < 0)
                startRow = 0;

            return startRow;
        }

        /// <summary>
        /// Retorna se para a condição, existe tal registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return _baseRepository.Any(filter);
        }

        /// <summary>
        /// Constrói busca no banco
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            return _baseRepository.Query(filter);
        }

        /// <summary>
        /// Busca páginada
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public PageResponse GetPaged(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>> filter
        )
        {
            return new PageResponse
            {
                Data = _baseRepository.GetPaged(
                    initialRow: CalcStartRow(
                        page,
                        pageSize
                    ),
                    pageSize: 10,
                    filter: filter
                    ),
                Count = Count(filter)
            };    
        }

        /// <summary>
        /// Busca páginada com parâmetros
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        public async Task<PaginatedList<TEntity>> GetPaged(PaginatedInputModel pagingParams)
        {
            var data = _baseRepository.Query(x => !x.Deleted)
                .ToList();

            #region [Filter]  
            if (pagingParams != null && pagingParams.FilterParams != null)
                if (pagingParams.FilterParams.Any())
                    data = Filter<TEntity>.FilteredData(
                        pagingParams.FilterParams, 
                        data
                    ).ToList() ?? data;
            #endregion

            #region [Sorting]  
            if (pagingParams != null && pagingParams.SortingParams != null)
                if (pagingParams.SortingParams.Count() > 0)
                    data = Sorting<TEntity>.SortData(
                        data,
                        pagingParams.SortingParams
                    ).ToList();
            #endregion

            #region [Paging]  
            return await PaginatedList<TEntity>.CreateAsync(
                data, 
                pagingParams.PageNumber, 
                pagingParams.PageSize
            );
            #endregion
        }

        /// <summary>
        /// Busca páginada com parâmetros
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task<PaginatedList<Object>> GetPaged(PaginatedInputModel pagingParams, Func<TEntity, Object> selector)
        {
            var data = _baseRepository.Query(x => !x.Deleted)
                .Select(selector)
                .ToList();

            #region [Filter]  
            if (pagingParams != null && pagingParams.FilterParams != null)
                if (pagingParams.FilterParams.Any())
                    data = Filter<Object>.FilteredData(
                        pagingParams.FilterParams,
                        data
                    ).ToList() ?? data;
            #endregion

            #region [Sorting]  
            if (pagingParams != null && pagingParams.SortingParams != null)
                if (pagingParams.SortingParams.Count() > 0)
                    data = Sorting<Object>.SortData(
                        data,
                        pagingParams.SortingParams
                    ).ToList();
            #endregion

            #region [Paging]  
            return await PaginatedList<Object>.CreateAsync(
                data,
                pagingParams.PageNumber,
                pagingParams.PageSize
            );
            #endregion
        }
    }
}
