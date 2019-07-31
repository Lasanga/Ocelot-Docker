using AutoMapper;
using BringIt.Restaurants.Core.Models;
using BringIt.Restaurants.Core.Models.Menus;
using BringIt.Restaurants.Core.Models.Menus.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Restaurants.Core.MapperProfile
{
    public class MapperPro : Profile
    {
        public MapperPro()
        {
            CreateMap<MenuInputDto, Menu>();
            CreateMap<Menu, MenuOutputDto>();

            CreateMap<RestaurantInputDto, BringIt.Restaurants.Core.Models.Restaurant>();
        }
    }
}
