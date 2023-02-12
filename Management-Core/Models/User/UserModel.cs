using Management.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Models.User
{
    public class UserModel: BaseModel
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<UserRoleModel>? UserRoleModel { get; set; }
    }

    public class UserRoleModel
    {
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }    
        public string? Description { get; set; }    
    }

    public class UserCartModel
    {
        public Guid CartId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
    }

    public class GuidObject
    {
        public Guid Id { get; set; }
    }
}
