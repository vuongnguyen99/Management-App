using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entities
{
    public class Organization: BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? ProjectName { get; set; }
        public bool Active { get; set; }
        public bool IsPrimary { get; set; }
        public virtual ICollection<OrganizationTeam> OrganizationTeams { get; set; }
    }
}
