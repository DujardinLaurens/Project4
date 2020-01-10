using BeerDB.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BeerDB.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BeerDBAPIContext>
    {
        BeerDBAPIContext IDesignTimeDbContextFactory<BeerDBAPIContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BeerDBAPIContext>();

            var connectionString = configuration.GetConnectionString("BeerDBAPIContext");

            builder.UseSqlServer(connectionString);

            return new BeerDBAPIContext(builder.Options);
        }

    }
}
