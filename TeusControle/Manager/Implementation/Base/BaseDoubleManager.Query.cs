using Core.Domain.Base;
using Core.Shared.Models.Enums;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Manager.Interfaces.Managers.Base;
using Manager.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager.Implementation.Base
{
    /// <summary>
    /// Classe de serviço genérica. CRUD genérico.
    /// </summary>
    public abstract partial class BaseDoubleManager<TEntity> : IBaseDoubleManager<TEntity> where TEntity : BaseDoubleEntity, new()
    {
        /// <summary>
        /// Retorna se para a condição, existe tal registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _baseRepository.AnyAsync(filter);
        }

        /// <summary>
        /// Busca quantidade de registros a partir do filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _baseRepository.CountAsync(filter);
        }

        /// <summary>
        /// Calcula qual a linha de start da listagem, conforme número da pagina atual e total de paginas
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int CalcStartRow(int pageNumber, int pageSize)
        {
            var startRow = (pageNumber - 1) * pageSize;
            if (startRow < 0)
                startRow = 0;

            return startRow;
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
        public async Task<PageResponse> GetPagedAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>> filter
        )
        {
            return new PageResponse
            {
                Data = await _baseRepository.GetPagedAsync(
                    initialRow: CalcStartRow(
                        page,
                        pageSize
                    ),
                    pageSize: 10,
                    filter: filter
                ),
                Count = await CountAsync(filter)
            };
        }

        /// <summary>
        /// Busca páginada com parâmetros
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <author>Sabyasachi Senapati</author>
        /// <returns></returns>
        public PaginatedResponse<TEntity> GetPaged(PaginatedRequest pagingParams)
        {
            var data = _baseRepository.Query(x => !x.Deleted).ToList();

            #region [Filter]  
            if (pagingParams != null && pagingParams.FilterParams.Any())
            {
                data = Filter<TEntity>.FilteredData(
                    pagingParams.FilterParams,
                    data
                ).ToList() ?? data;
            }
            #endregion

            #region [Sorting]
            if (
                pagingParams != null &&
                pagingParams.SortingParams.Count() > 0 &&
                Enum.IsDefined(
                    typeof(SortEnum),
                    pagingParams.SortingParams.Select(x => x.SortOrder)
                )
            )
            {
                data = Sorting<TEntity>.SortData(
                    data,
                    pagingParams.SortingParams
                ).ToList();
            }
            #endregion

            #region [Paging]  
            return PaginatedResponse<TEntity>.Create(
                data,
                pagingParams.PageNumber,
                pagingParams.PageSize
            );
            #endregion
        }
    }
}
