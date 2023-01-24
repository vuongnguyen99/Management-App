using Management.Data.Configuration;
using Management.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Data
{
    public class ManagementDbContext: DbContext
    {
        public ManagementDbContext(DbContextOptions<ManagementDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationTeamConfiguration());
            modelBuilder.ApplyConfiguration(new UserProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseModel && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseModel)entityEntry.Entity).ModifiedDate = DateTime.UtcNow;
                ((BaseModel)entityEntry.Entity).ModifiedBy = "UnKnown";

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseModel)entityEntry.Entity).CreateDate = DateTime.UtcNow;
                    ((BaseModel)entityEntry.Entity).CreateBy = "UnKnown";
                    ((BaseModel)entityEntry.Entity).ModifiedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseModel entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreateDate = DateTime.UtcNow;
                                entityReference.CreateBy = "Unknown";
                                entityReference.ModifiedDate = DateTime.UtcNow;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreateDate).IsModified = false;

                                entityReference.ModifiedDate = DateTime.UtcNow;
                                entityReference.ModifiedBy = "Unknown";
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationTeam> OrganizationTeams { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
