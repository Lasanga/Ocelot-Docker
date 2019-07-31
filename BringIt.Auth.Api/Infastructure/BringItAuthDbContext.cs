using BringIt.Auth.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BringIt.Auth.Api.Infastructure
{
    public class BringItAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public BringItAuthDbContext(DbContextOptions<BringItAuthDbContext> options)
           : base(options)
        {
        }
    }
}
