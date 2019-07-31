using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Restaurants.Core.Models
{
    public class RestaurantOutputDto
    {
        public int Id { get; set; }

        public decimal Latitutde { get; set; }

        public decimal Longitude { get; set; }

        public string DisplayName { get; set; }

        public string City { get; set; }
    }
}
