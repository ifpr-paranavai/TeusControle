using Core.Domain;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Core.Shared.Models.User;
using Manager.Interfaces.Managers.Base;
using System;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IUserManager : IBaseManager<User>
    {
        Task<UserModel> DeleteById(int id);
        Task<UserModel> GetById(int id);
        new PaginatedResponse<UserPagedModel> GetPaged(PaginatedRequest pagingParams);
        Task<UserModel> Insert(CreateUserModel newUser);
        Task<UserModel> Update(UpdateUserModel updatedUser);
        Task<string> ValidatePasswordGenerateTokenAsync(LoginRequest login);
    }
}
