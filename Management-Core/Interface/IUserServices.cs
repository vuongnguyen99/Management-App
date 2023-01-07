using Management_Core.Models.Paging;
using Management_Core.Models.User;

namespace Management_Core.Interface
{
    public interface IUserServices
    {
        Task<CreateUserResponse> CreateNewUser(CreateUserRequest request,Guid? RolesId ,CancellationToken cancellationToken);
        Task<CreateUserResponse> UpdateUser(Guid Id, UpdateUser request, CancellationToken cancellationToken);
        Task<UserModel> GetUsersByIdAsync(Guid? userId, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserModel>> GetUsersByFilterPageAsync(Pagination paging, CancellationToken cancellationToken);
        Task DeleteUsersAsync(Guid userId, CancellationToken cancellationToken);
        Task DeleteListUser(List<GuidObject> GuidObject, CancellationToken cancellationToken);
    }
}
