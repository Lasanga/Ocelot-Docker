using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BringIt.Auth.Api.Core;
using BringIt.Restaurant.Services.MenuManager;
using BringIt.Restaurant.Services.Restaurant;
using BringIt.Restaurants.Core.Models;
using BringIt.Restaurants.Core.Models.Menus;
using BringIt.Restaurants.Core.Models.Menus.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BringIt.Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMenuManager _menuManager;
        private readonly IRestaurantManager _restaurantManager;
        private readonly IMapper _mapper;

        public RestaurantsController(IMenuManager menuManager, IMapper mapper, IRestaurantManager restaurantManager)
        {
            _menuManager = menuManager;
            _mapper = mapper;
            _restaurantManager = restaurantManager;
        }

        [HttpPost]
        [Route("CreateMenu")]
        [Authorize(Roles = StaticRoleNames.Admin)]
        public async Task InsertMenu(MenuInputDto input)
        {
            var menu = _mapper.Map<Menu>(input);
            await _menuManager.InsertAsync(menu);
        }

        [HttpGet]
        [Route("GetByRestaurantId")]
        [Authorize]
        public async Task<MenuOutputDto> GetByRestaurantId(int id)
        {
            var result = await _menuManager.GetByRestId(id);
            return _mapper.Map<MenuOutputDto>(result);
        }

        [HttpPost]
        [Route("CreateRestaurant")]
        [Authorize(Roles = StaticRoleNames.Admin)]
        public async Task InsertRestaurant(RestaurantInputDto input)
        {
            var restaurant = _mapper.Map<BringIt.Restaurants.Core.Models.Restaurant>(input);
            await _restaurantManager.InsertAsync(restaurant);
        }

        [HttpGet]
        [Route("GetRestaurants")]
        [Authorize]
        public async Task<List<RestaurantOutputDto>> GetRestaurant()
        {
            var result = await _restaurantManager.GetAllAsync();
            return result.Select(x => new RestaurantOutputDto()
            {
                City = x.City,
                DisplayName = x.DisplayName,
                Latitutde = x.Latitutde,
                Longitude = x.Longitude,
                Id = x.Id
            }).ToList();
        }

        [HttpDelete]
        [Route("DeleteRestaurant")]
        [Authorize(Roles =StaticRoleNames.Admin)]
        public async Task DeleteRestaurant(int id)
        {
            await _restaurantManager.DeleteAsync(id);
        }
    }
}