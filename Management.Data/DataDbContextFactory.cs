using Management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataDbContextFactory : IDesignTimeDbContextFactory<ManagementDbContext>
    {
        public ManagementDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ManagementDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ManagementDev;Username=postgres;Password=Nguyenvuong1999;");

            return new ManagementDbContext(optionsBuilder.Options);
        }
    }
}
