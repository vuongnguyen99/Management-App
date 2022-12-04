using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entites
{
    public class UserRole:BaseModel
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public Users? Users { get; set; }
        public Role? Roles { get; set; }
    }
}
