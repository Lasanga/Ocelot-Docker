using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BringIt.Restaurant.Infrastrucutre.UnitOfWork;
using BringIt.Restaurants.Core.Models;

namespace BringIt.Restaurant.Services.Restaurant
{
    public class RestaurantManager : IRestaurantManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _unitOfWork.Restaurants.GetAllAsync();
            if (result != null)
            {
                var filter = result.Where(x => x.Id == id).FirstOrDefault();

                if (filter != null)
                {
                    filter.IsDeleted = true;
                    filter.DeletionTime = DateTime.Now;

                    _unitOfWork.Restaurants.DeleteAsync(filter);
                }
            }
        }

        public async Task<List<Restaurants.Core.Models.Restaurant>> GetAllAsync()
        {
            var result = await _unitOfWork.Restaurants.GetAllAsync();
            return result.ToList();
        }

        public async Task<Restaurants.Core.Models.Restaurant> GetAsync(int id)
        {
            var result = await _unitOfWork.Restaurants.GetAsync(id);
            return result;
        }

        public async Task InsertAsync(Restaurants.Core.Models.Restaurant restaurant)
        {
            restaurant.CreationTime = DateTime.Now;
            await _unitOfWork.Restaurants.InsertAsync(restaurant);
        }

        public async Task UpdateAsync(Restaurants.Core.Models.Restaurant restaurant)
        {
            var result = await _unitOfWork.Restaurants.UpdateAsync(restaurant);
        }
    }
}
