using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entites
{
    public class Role:BaseModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
