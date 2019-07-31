using BringIt.Restaurants.Core.Models.Menus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BringIt.Restaurant.Services.MenuManager
{
    public interface IMenuManager
    {
        Task InsertAsync(Menu menu);
        Task UpdateAsync(Menu menu);
        Task DeleteAsync(string id);
        Task<Menu> GetAsync(string id);
        Task<List<Menu>> GetAllAsync();
        Task<Menu> GetByRestId(int id);
    }
}
