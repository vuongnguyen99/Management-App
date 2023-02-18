using Management.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Management.Data.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.HasOne(x => x.Products).WithMany(x => x.Images).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.User).WithMany(x => x.Images).HasForeignKey(x => x.UserId);
        }
    }
}
