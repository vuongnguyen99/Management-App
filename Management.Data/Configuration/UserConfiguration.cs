using Management.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Management.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
            builder.HasIndex(x => x.Username).IsUnique(true);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.LoginFailedCount).HasDefaultValue(0);
            builder.Property(x => x.Avatar).IsRequired(false);
            builder.Property(x => x.Active);
        }
    }


}
