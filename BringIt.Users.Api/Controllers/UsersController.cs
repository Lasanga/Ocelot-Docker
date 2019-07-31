using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BringIt.Users.Core.Models;
using BringIt.Users.Core.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BringIt.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task Register([FromBody]UserRegisterInputDto input)
        {
            var result = new IdentityResult();
            var user = new ApplicationUser { UserName = input.UserName, Email = input.EmailAddress, Age = input.Age };

            if (!string.IsNullOrEmpty(input.VehicleNumber))
            {
                user = new ApplicationUser { UserName = input.UserName, Email = input.EmailAddress, Age = input.Age, VehicleNumber = input.VehicleNumber };

                result = await _userManager.CreateAsync(user, input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, StaticRoleNames.Driver);
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", StaticRoleNames.Driver));
                }

            }
            else
            {
                result = await _userManager.CreateAsync(user, input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, StaticRoleNames.Client);
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", StaticRoleNames.Client));
                }

            }

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
        }

        [HttpGet]
        [Route("GetUsers")]
        [Authorize(Roles = StaticRoleNames.Admin)]
        public async Task<List<UserOutputDto>> GetUsers()
        {
            var users = _userManager.Users.ToList().Where(x => string.IsNullOrEmpty(x.VehicleNumber));

            return users?.Select(x => new UserOutputDto()
            {
                EmailAddress = x.Email,
                Id = x.Id,
                UserName = x.UserName,
                Age = x.Age
            }).ToList();

        }

        [HttpGet]
        [Route("GetDrivers")]
        [Authorize(Roles = StaticRoleNames.Admin)]
        public async Task<List<UserOutputDto>> GetDrivers()
        {
            var users = _userManager.Users.ToList().Where(x => !string.IsNullOrEmpty(x.VehicleNumber));

            return users?.Select(x => new UserOutputDto()
            {
                EmailAddress = x.Email,
                Id = x.Id,
                UserName = x.UserName,
                Age = x.Age,
                VehicleNumber = x.VehicleNumber
            }).ToList();

        }
    }
}