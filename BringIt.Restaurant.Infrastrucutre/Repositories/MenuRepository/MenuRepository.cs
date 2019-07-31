using BringIt.Restaurants.Core.Models.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Restaurant.Infrastrucutre.Repositories.MenuRepository
{
    public class MenuRepository: Repository<Menu>, IMenuRepository
    {
        public MenuRepository(BringItRestuarantDbContext context)
            :base(context)
        {

        }
    }
}
