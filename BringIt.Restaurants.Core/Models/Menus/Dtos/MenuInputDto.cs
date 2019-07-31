using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Restaurants.Core.Models.Menus.Dtos
{
    public class MenuInputDto
    {
        public int RestaurantId { get; set; }

        public List<MenuList> List { get; set; }
    }
}
