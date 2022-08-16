using Core.Domain;
using Manager.Interfaces.Managers.Base;

namespace Manager.Interfaces
{
    public interface IProductEntryManager : IBaseDoubleManager<ProductEntry>
    {
        /*Task<EntryModel> DeleteById(int id, int );
        Task<EntryModel> GetById(int id);
        new PaginatedResponse<EntryPagedModel> GetPaged(PaginatedRequest pagingParams);
        Task<EntryModel> Insert(CreateEntryModel newUser);
        Task<EntryModel> Update(UpdateEntryModel updatedUser);*/
    }
}
