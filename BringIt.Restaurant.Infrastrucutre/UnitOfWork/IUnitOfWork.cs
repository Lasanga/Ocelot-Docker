using BringIt.Restaurant.Infrastrucutre.Repositories.MenuRepository;
using BringIt.Restaurant.Infrastrucutre.Repositories.RestaurantRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Restaurant.Infrastrucutre.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IRestaurantRepository Restaurants { get; }
        IMenuRepository Menus { get; }

        int Complete();
    }
}
