using Core.Domain;
using Core.Shared.Models.Product;
using Core.Shared.Models.Request;
using Manager.Interfaces.Managers.Base;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IProductManager : IBaseManager<Product>
    {
        Task DeleteById(int id);
        Task<object> GetById(long id);
        Task<object> GetPaged(PaginatedRequest pagingParams);
        Task<ProductModel> Insert(CreateProductModel newUser);
        Task<ProductModel> Update(UpdateProductModel updatedUser);
    }
}
