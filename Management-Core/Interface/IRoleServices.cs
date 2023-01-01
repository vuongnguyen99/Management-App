using Management.Data.Entities;
using Management_Core.Models.Role;

namespace Management_Core.Interface
{
    public interface IRoleServices
    {
        Task<CreateRoleResponse> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken);
    }
}
