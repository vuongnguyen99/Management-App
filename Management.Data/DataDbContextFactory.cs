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
            optionsBuilder.UseNpgsql("Host=localhost;Database={YOUR_DATABASE_NAME};Username=postgres;Password={YOUR_PASSWORD};");

            return new ManagementDbContext(optionsBuilder.Options);
        }
    }
}
