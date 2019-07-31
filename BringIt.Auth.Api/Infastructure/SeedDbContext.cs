using BringIt.Auth.Api.Core;
using BringIt.Auth.Api.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BringIt.Auth.Api.Infastructure
{
    public static class SeedDbContext
    {
        public static async Task Seed(BringItAuthDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = StaticRoleNames.Admin },
                new IdentityRole { Name = StaticRoleNames.Client },
                new IdentityRole { Name = StaticRoleNames.Driver },
            };

            foreach (var role in roles)
            {
                var result = await roleManager.FindByNameAsync(role.Name);
                if (result == null)
                {
                    await roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = role.Name
                    });
                }
            }

            #region SeedUsers

            var adminUsers = await userManager.GetUsersInRoleAsync(StaticRoleNames.Admin);
            if (adminUsers.Count == 0)
            {
                var user = new ApplicationUser()
                {
                    Email = "admin@intellect.com",
                    UserName = "admin"
                };

                var result = await userManager.CreateAsync(user, "123qwe");

                result = userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Admin admin"),
                        new Claim(JwtClaimTypes.Email, "admin@bringIt.com"),

                }).Result;

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "123qwe");
                    await userManager.AddToRoleAsync(user, StaticRoleNames.Admin);
                }
            }
            #endregion

        }
    }
}
