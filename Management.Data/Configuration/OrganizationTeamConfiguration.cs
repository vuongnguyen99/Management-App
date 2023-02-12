using Management.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Configuration
{
    public class OrganizationTeamConfiguration : IEntityTypeConfiguration<OrganizationTeam>
    {
        public void Configure(EntityTypeBuilder<OrganizationTeam> builder)
        {
            builder.ToTable("OrganizationTeams");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.ParentOrganizationNodeId);
            builder.HasOne(x => x.Users).WithMany(x => x.OrganizationTeams).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Organizations).WithMany(x => x.OrganizationTeams).HasForeignKey(x => x.OrganizationId);
           builder.HasOne(x => x.Roles).WithMany(x => x.OrganizationTeams).HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.Products).WithMany(x => x.OrganizationTeams).HasForeignKey(x => x.ProductId);
        }
    }
}
