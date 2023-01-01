using Management.Data;
using Management.Data.Entities;
using Management_Core.Interface;
using Management_Core.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Services
{
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
                Description = request.Description
            };
            await _dbContext.Roles.AddAsync(newRole, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new CreateRoleResponse
            {
                Id = newRole.Id,
                Name = request.Name,
                Description = request.Description,
                CreateBy = newRole.CreateBy,
                CreateDate = newRole.CreateDate,
                ModifiedBy = newRole.ModifiedBy,
                ModifiedDate = newRole.ModifiedDate,
            };
        }
    }
}
