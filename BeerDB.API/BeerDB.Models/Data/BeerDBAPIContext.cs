using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeerDB.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BeerDB.API.Models
{
    public class BeerDBAPIContext : IdentityDbContext<IdentityUser>
    {
        public BeerDBAPIContext (DbContextOptions<BeerDBAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Reactie> Reactie { get; set; }
    }
}
