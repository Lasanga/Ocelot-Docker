using BringIt.Orders.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Orders.Core.Models.Dtos
{
    public class OrderOutputDto
    {
        public string Id { get; set; }
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public int DriverId { get; set; }
        public OrderState State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Item> ItemList { get; set; }
    }
}
