using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entities
{
    public class OrganizationTeam: BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ParentOrganizationNodeId { get; set; }
        public Guid UserId { get; set; }
        public Guid? RoleId { get; set; }
        public virtual Role Roles { get; set; }
        public virtual Organization Organizations { get; set; }
        public virtual Product Products { get; set; }
        public virtual User Users { get; set; }
    }
}
