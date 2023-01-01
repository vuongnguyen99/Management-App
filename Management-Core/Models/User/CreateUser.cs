using Management.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Models.User
{
    public class CreateUserRequest
    {
#nullable disable
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
        public int LoginFailedCount { get; set; }
        public List<CreateUserRole> UserRoles { get; set; }
    }
    public class CreateUserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class CreateUserResponse : BaseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
        public List<CreateUserRoleResponse> UserRolesResponse { get; set; }
    }
    public class CreateUserRoleResponse
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
