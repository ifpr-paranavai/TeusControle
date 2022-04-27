using System.Threading.Tasks;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Interfaces.Services
{
    public interface IDisposalsService : IBaseService<Disposals>
    {
        /// <summary>
        /// Cria um registro de saída de produtos
        /// </summary>
        /// <param name="disposalModel"></param>
        /// <returns></returns>
        ResponseMessages<object> Insert(CreateDisposalModel disposalModel);

        /// <summary>
        /// Atualiza um registro de saída de produtos
        /// </summary>
        /// <param name="disposalModel"></param>
        /// <returns></returns>
        ResponseMessages<object> Update(UpdateDisposalModel disposalModel);

        /// <summary>
        /// Insere um item de produto a saída
        /// </summary>
        /// <param name="productDisposal"></param>
        /// <returns></returns>
        ResponseMessages<object> InsertProductDisposalItem(CreateProductDisposalsModel productDisposal);

        /// <summary>
        /// Remove um item de produto a entrada
        /// </summary>
        /// <param name="disposalId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        ResponseMessages<object> DeleteProductDisposalItem(
            long disposalId,
            long productId
        );

        /// <summary>
        /// Fluxo para fechar uma saída de produto e atualizar quantidade em estoque de produtos
        /// </summary>
        /// <param name="disposalId"></param>
        /// <returns></returns>
        ResponseMessages<object> CloseDisposal(long disposalId);

        /// <summary>
        /// Busca um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseMessages<object> GetById(long id);

        /// <summary>
        /// Busca lista paginada de todas as saídas de produtos cadastradas
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ResponseMessages<object> Get(
            int pageNumber = 1,
            int pageSize = 10
        );

        /// <summary>
        /// Deleta uma saída
        /// </summary>
        /// <param name="disposalId"></param>
        /// <returns></returns>
        ResponseMessages<object> Delete(long disposalId);

        /// <summary>
        /// Busca lista de todos saídas levando em consideração parametros de filtragem e ordenação
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        Task<ResponseMessages<object>> Get(PaginatedInputModel pagingParams);
    }
}
