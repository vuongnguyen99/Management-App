using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entites
{
    public class Users: BaseModel
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }   
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool Gender { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
    }
}
