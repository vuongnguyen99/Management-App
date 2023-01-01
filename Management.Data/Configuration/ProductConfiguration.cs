using Management.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Code).IsRequired();
        }
    }
}
