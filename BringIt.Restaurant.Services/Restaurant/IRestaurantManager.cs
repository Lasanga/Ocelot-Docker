using BringIt.Restaurants.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BringIt.Restaurant.Services.Restaurant
{
    public interface IRestaurantManager
    {
        Task InsertAsync(BringIt.Restaurants.Core.Models.Restaurant restaurant);
        Task UpdateAsync(BringIt.Restaurants.Core.Models.Restaurant restaurant);
        Task DeleteAsync(int id);
        Task<BringIt.Restaurants.Core.Models.Restaurant> GetAsync(int id);
        Task<List<BringIt.Restaurants.Core.Models.Restaurant>> GetAllAsync();
    }
}
