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
    public class UserProductConfiguration : IEntityTypeConfiguration<UserProduct>
    {
        public void Configure(EntityTypeBuilder<UserProduct> builder)
        {
            builder.ToTable("UserProducts");
            builder.HasKey(up => new { up.UserId, up.ProductId });

            builder.HasOne(u => u.User)
                   .WithMany(u => u.UserProducts)
                   .HasForeignKey(u => u.UserId)
                   .HasConstraintName("FK_UserProduct_User_UserId");

            builder.HasOne(ur => ur.Product)
                   .WithMany(r => r.UserProducts)
                   .HasForeignKey(ur => ur.ProductId)
                   .HasConstraintName("FK_UserProduct_Product_ProductId");

        }
    }
}
