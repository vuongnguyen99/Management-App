using Management.Common.Common;
using Management.Common.Exception;
using Management.Core.Models.Role;
using Management.Data;
using Management.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Core.Services
{
    public interface IRoleServices
    {
        Task<CreateRoleResponse> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken);
        Task<GetRoleResponse> GetRoleById(Guid roleId, CancellationToken cancellationToken);
        Task<ICollection<AssignRoleForUser>> GetAllRoleForAssign( CancellationToken cancellationToken);

    }
    public class RoleServices : IRoleServices
    {
        private readonly ManagementDbContext _dbContext;
        public RoleServices(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateRoleResponse> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var newRole = new Role()
            {
                Name = request.Name,
                Description = request.Description,
            };
            await _dbContext.Roles.AddAsync(newRole);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateRoleResponse
            {
                Id = newRole.Id,
                Name = newRole.Name,
                Description = newRole.Description,
                CreateBy = newRole.CreateBy,
                CreateDate = newRole.CreateDate,
                ModifiedBy = newRole.ModifiedBy,
                ModifiedDate = newRole.ModifiedDate
            };
        }

        public async Task<ICollection<AssignRoleForUser>> GetAllRoleForAssign(CancellationToken cancellationToken)
        {
            var getRole = await _dbContext.Roles.AsNoTracking().ToListAsync(cancellationToken);
            return getRole.Select(x => new AssignRoleForUser
            {
                roleId = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<GetRoleResponse> GetRoleById(Guid roleId, CancellationToken cancellationToken)
        {
            var getRoleById = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
            if(getRoleById == null)
            {
                throw new NotFoundException($"The role id {roleId} is not found.");
            }
            return new GetRoleResponse
            {
                Id = getRoleById.Id,
                Name = getRoleById.Name,
                Description = getRoleById.Description,
                CreateDate = DateTimeHelper.ConvertDateTimeLocalToUTC(getRoleById.CreateDate),
                CreateBy = getRoleById.CreateBy,
                ModifiedBy = getRoleById.ModifiedBy,
                ModifiedDate = DateTimeHelper.ConvertDateTimeLocalToUTC(getRoleById.ModifiedDate),
            };
        }
    }
}
