using Management.Data.Entites;
using Management_Core.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Interface
{
    public interface IRoleServices
    {
        Task<Role> CreateRole(CreateRole request, CancellationToken cancellationToken);
    }
}
