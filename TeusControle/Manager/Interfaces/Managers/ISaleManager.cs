using Core.Domain;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Core.Shared.Models.Sale;
using Manager.Interfaces.Managers.Base;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface ISaleManager : IBaseManager<Sale>
    {
        Task<SaleModel> DeleteById(int id);
        Task<object> GetById(int id);
        new PaginatedResponse<SalePagedModel> GetPaged(PaginatedRequest pagingParams);
        Task<SaleModel> Insert(CreateSaleModel newSale);
        Task<SaleModel> Update(UpdateSaleModel updatedSalel);
    }
}
