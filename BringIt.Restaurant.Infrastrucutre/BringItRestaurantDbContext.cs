using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Restaurant.Infrastrucutre
{
    public class BringItRestuarantDbContext : DbContext
    {
        public virtual DbSet<BringIt.Restaurants.Core.Models.Restaurant> Restaurants { get; set; }

        public BringItRestuarantDbContext(DbContextOptions<BringItRestuarantDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BringIt.Restaurants.Core.Models.Restaurant>().HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
