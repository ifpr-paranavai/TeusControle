using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Interfaces.Services
{
    public interface IProductEntriesService : IBaseDoubleService<ProductEntries>
    {
        /// <summary>
        /// Insere um item de produto a entrada
        /// </summary>
        /// <returns></returns>
        ResponseMessages<object> InsertProductEntryItem(CreateProductEntriesModel productEntry);

        /// <summary>
        /// Remove um item de protudo da entrada
        /// </summary>
        /// <param name="entry_id"></param>
        /// <param name="product_id"></param>
        /// <returns></returns>
        ResponseMessages<object> DeleteProductEntryItem(
            long entry_id,
            long product_id
        );
    }
}
