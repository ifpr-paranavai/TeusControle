using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Interfaces.Services
{
    public interface IProductDisposalsService : IBaseDoubleService<ProductDisposals>
    {
        /// <summary>
        /// Insere um item de produto a saída
        /// </summary>
        /// <returns></returns>
        ResponseMessages<object> InsertProductDisposalsItem(CreateProductDisposalsModel productDisposal);

        /// <summary>
        /// Remove um item de protudo da saída
        /// </summary>
        /// <param name="disposalId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        ResponseMessages<object> DeleteProductDisposalItem(
            long disposalId,
            long productId
        );
    }
}
