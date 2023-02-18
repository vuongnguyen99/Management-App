using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Models.User
{
    public class UpdateUser
    {
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public bool Gender { get; set; }
        public bool Active { get; set; }
    }
}
