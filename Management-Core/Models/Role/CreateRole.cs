using Management.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Models.Role
{
    public class CreateRole: BaseModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
