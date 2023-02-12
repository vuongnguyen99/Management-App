using Management.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Models.Role
{
    public class GetRoleResponse: BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? OrganizationId { get; set; }
        public string? Description { get; set; }
    }
}
