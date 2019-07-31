using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Restaurants.Core.Models.Menus
{
    public class Menu
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int RestaurantId { get; set; }

        public List<MenuList> List { get; set; }
    }
}
