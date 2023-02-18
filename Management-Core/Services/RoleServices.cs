using Management.Data;
using Management.Data.Entities;
using Management_Common.Common;
using Management_Common.Exception;
using Management_Core.Models.Role;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Services
{
    public interface IRoleServices
    {
        Task<string> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken);
        Task<GetRoleResponse> GetRoleById(Guid roleId, CancellationToken cancellationToken);

    }
    public class RoleServices : IRoleServices
    {
        private readonly ManagementDbContext _dbContext;
        public RoleServices(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var newRole = new Role()
            {
                Name = request.Name,
                Description = request.Description,
            };
            await _dbContext.Roles.AddAsync(newRole);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "Create Successfully";
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
