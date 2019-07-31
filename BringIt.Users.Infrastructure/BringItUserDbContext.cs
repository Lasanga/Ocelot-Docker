using BringIt.Users.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Users.Infrastructure
{
    public class BringItUserDbContext: IdentityDbContext<ApplicationUser>
    {
        public BringItUserDbContext(DbContextOptions<BringItUserDbContext> options)
           : base(options)
        {
        }
    }
}
