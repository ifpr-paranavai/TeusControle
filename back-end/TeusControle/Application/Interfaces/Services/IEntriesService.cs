using System.Threading.Tasks;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Interfaces.Services
{
    public interface IEntriesService : IBaseService<Entries>
    {
        /// <summary>
        /// Cria um registro de entrada de produtos
        /// </summary>
        /// <returns></returns>
        ResponseMessages<object> CreateEntry(CreateEntryModel entryModel);

        /// <summary>
        /// Atualiza um registro de entrada de produtos
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        ResponseMessages<object> Update(UpdateEntryModel entry);

        /// <summary>
        /// Insere um item de produto a entrada
        /// </summary>
        /// <param name="productEntry"></param>
        /// <returns></returns>
        ResponseMessages<object> InsertProductEntryItem(CreateProductEntriesModel productEntry);

        /// <summary>
        /// Remove um item de produto a entrada
        /// </summary>
        /// <param name="entry_id"></param>
        /// <param name="product_id"></param>
        /// <returns></returns>
        ResponseMessages<object> DeleteProductEntryItem(
            long entry_id,
            long product_id
        );

        /// <summary>
        /// Fluxo para fechar uma entrada de produto e atualizar quantidade em estoque de produtos
        /// </summary>
        /// <param name="entry_id"></param>
        /// <returns></returns>
        ResponseMessages<object> CloseEntry(long entry_id);

        /// <summary>
        /// Busca um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseMessages<object> GetById(long id);

        /// <summary>
        /// Busca lista paginada de todas as entradas de produtos cadastradas
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ResponseMessages<object> Get(
            int pageNumber = 1,
            int pageSize = 10
        );

        /// <summary>
        /// Deleta uma entrada
        /// </summary>
        /// <param name="entry_id"></param>
        /// <returns></returns>
        ResponseMessages<object> DeleteEntry(long entry_id);

        /// <summary>
        /// Busca lista de todos entradas levando em consideração parametros de filtragem e ordenação
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        Task<ResponseMessages<object>> Get(PaginatedInputModel pagingParams);
    }
}
