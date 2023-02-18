using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entities
{
    public class User: BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public bool Active { get; set; }
        public int? LoginFailedCount { get; set; }
        public Guid? ManagedBy { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<UserProduct>? UserProducts { get; set; }
        public virtual ICollection<OrganizationTeam>? OrganizationTeams { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
    }
}
