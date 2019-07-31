using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Restaurant.Infrastrucutre.Repositories.RestaurantRepository
{
    public class RestaurantRepository: Repository<BringIt.Restaurants.Core.Models.Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(BringItRestuarantDbContext context)
            : base(context)
        {

        }
    }
}
