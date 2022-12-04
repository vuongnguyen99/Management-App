using Management.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Management.Data
{
    public class ProductEntitlementsContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public ProductEntitlementsContext()
        {
        }

        public ProductEntitlementsContext(DbContextOptions<ProductEntitlementsContext> options)
            : base(options)
        {
        }
#nullable disable
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductInImages> ProductInImages { get; set; }
        public virtual DbSet<ProductOwners> ProductOwners { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;database=ManagementEntitlements;Username=postgres;Password=Nguyenvuong1999;");
            base.OnConfiguring(optionsBuilder);
        }


        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseModel entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreateDate = DateTime.UtcNow;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.ModifiedDate = DateTime.UtcNow;
                                break;
                            }
                    }
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
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreateDate).IsModified = false;

                                entityReference.ModifiedDate = DateTime.UtcNow;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(x => x.Id);

                entity.Property(e => e.Email).HasMaxLength(255).IsRequired(true);

                entity.Property(e => e.FirstName).HasMaxLength(255).IsRequired(false);

                entity.Property(e => e.LastName).HasMaxLength(255).IsRequired(false); ;

                entity.Property(e => e.Password).HasMaxLength(255).IsRequired(false);

                entity.Property(e => e.Active);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(x => x.Id);

                entity.Property(e => e.Name).HasMaxLength(255).IsRequired(true);

                entity.Property(e => e.Price);

                entity.Property(e => e.ViewCount);
                entity.HasOne(e=>e.ProductOwners).WithMany(e=>e.Products).HasForeignKey(e=>e.ProductOwnerId);

            });

            modelBuilder.Entity<ProductOwners>(entity =>
            {
                entity.ToTable("ProductOwner");

                entity.HasIndex(x => x.Id);

                entity.Property(e => e.Name).HasMaxLength(255).IsRequired(true);

                entity.Property(e => e.Follower);

                entity.Property(e => e.TotalProduct);
               
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.ToTable("Image");

                entity.HasIndex(x => x.Id);

                entity.Property(e => e.Name).HasMaxLength(255).IsRequired(true);

                entity.Property(e => e.ImagePath).HasMaxLength(255).IsRequired(false);

                entity.Property(e => e.Size);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.HasIndex(x => x.Id);

                entity.Property(e => e.Quantity);

                entity.HasOne(e => e.Products).WithMany(e => e.Carts).HasForeignKey(x => x.ProductId);

                entity.HasOne(e => e.Users).WithMany(e => e.Carts).HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<ProductInImages>(entity =>
            {
                entity.ToTable("ProductInImage");

                entity.HasIndex(x => x.Id);

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.ImageId);

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.ProductInImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Images)
                    .WithMany(p => p.ProductInImages)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasIndex(x => x.Id);

                entity.HasKey(x => x.Id);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
