using Management.Data;
using Management.Data.Entites;
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
        private readonly ProductEntitlementsContext _dbContext;
        public RoleServices(ProductEntitlementsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Role> CreateRole(CreateRole request, CancellationToken cancellationToken)
        {
            var newRole = new Role()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreateBy = "UnKnown",
                CreateDate = DateTime.UtcNow,
                ModifiedBy = "UnKnown",
                ModifiedDate = DateTime.UtcNow,
            };
            await _dbContext.Roles.AddAsync(newRole, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new Role
            {
                Id = newRole.Id,
                Name = request.Name,
                CreateBy = newRole.CreateBy,
                CreateDate = newRole.CreateDate,
                ModifiedBy = newRole.ModifiedBy,
                ModifiedDate = newRole.ModifiedDate,
            };
        }
    }
}
