using BringIt.Orders.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Orders.Core.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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
