﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entities
{
    public class Product: BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool Active { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
        public virtual ICollection<UserProduct> UserProducts { get; set; }
        public virtual ICollection<OrganizationTeam> OrganizationTeams { get; set; }
    }
}
