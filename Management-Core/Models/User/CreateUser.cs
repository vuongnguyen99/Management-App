using Management.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Models.User
{
    public class CreateUser
    {
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool Gender { get; set; }
        public bool Active { get; set; }
        public List<UserRole>? Roles { get; set; }
    }

    public class CreateUserResponse
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
        public ICollection<Roles>? Roles { get; set; }
    }

    public class Roles
    {
        public Guid RoleId { get; set; }
    }
}
