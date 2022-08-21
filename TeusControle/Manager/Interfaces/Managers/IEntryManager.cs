using Core.Domain;
using Core.Shared.Models.Entry;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Manager.Interfaces.Managers.Base;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IEntryManager : IBaseManager<Entry>
    {
        Task<EntryModel> DeleteById(int id);
        Task<object> GetById(int id);
        new PaginatedResponse<EntryPagedModel> GetPaged(PaginatedRequest pagingParams);
        Task<EntryModel> Insert(CreateEntryModel newUser);
        Task<EntryModel> Update(UpdateEntryModel updatedUser);
    }
}
