using Core.Domain;
using Core.Shared.Models.Product;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Manager.Interfaces.Managers.Base;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IProductManager : IBaseManager<Product>
    {
        Task<ProductModel> DeleteById(int id);
        Task<ProductModel> GetById(int id);
        new PaginatedResponse<ProductPagedModel> GetPaged(PaginatedRequest pagingParams);
        Task<ProductModel> Insert(CreateProductModel newUser);
        Task<ProductModel> Update(UpdateProductModel updatedUser);
        Task<object> GetProductByCode(string gtinCode);

    }
}
