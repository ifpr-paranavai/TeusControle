using Core.Domain;
using Core.Shared.Models.Request;
using Core.Shared.Models.User;
using Manager.Interfaces.Managers.Base;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IUserManager : IBaseManager<User>
    {
        Task DeleteById(int id);
        Task<object> GetById(long id);
        Task<object> GetPaged(PaginatedRequest pagingParams);
        Task<UserModel> Insert(CreateUserModel newUser);
        Task<UserModel> Update(UpdateUserModel updatedUser);
        Task<string> ValidatePasswordGenerateTokenAsync(LoginRequest login);
    }
}
