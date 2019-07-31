using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BringIt.Restaurants.Core.Models.Menus;
using MongoDB.Driver;

namespace BringIt.Restaurant.Services.MenuManager
{
    public class MenuManager : IMenuManager
    {
        private readonly IMongoCollection<Menu> _menuMongo;

        public MenuManager()
        {
            //var client = new MongoClient("mongodb://localhost:27017");
            var client = new MongoClient("mongodb://lasanga:lasanga%40123@cluster0-shard-00-00-9yhhw.azure.mongodb.net:27017,cluster0-shard-00-01-9yhhw.azure.mongodb.net:27017,cluster0-shard-00-02-9yhhw.azure.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true");
            var database = client.GetDatabase("BringItDb");

            _menuMongo = database.GetCollection<Menu>("MenuMongo");
        }

        public async Task DeleteAsync(string id)
        {
           await _menuMongo.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Menu>> GetAllAsync()
        {
            var result = await _menuMongo.FindAsync(x => true);

            return result.ToList();
        }

        public async Task<Menu> GetAsync(string id)
        {
            var result = await _menuMongo.FindAsync(x => x.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<Menu> GetByRestId(int id)
        {
            var result = await _menuMongo.FindAsync(x => x.RestaurantId == id);
            return result.FirstOrDefault();
        }

        public async Task InsertAsync(Menu menu)
        {
            await _menuMongo.InsertOneAsync(menu);
        }

        public async Task UpdateAsync(Menu menu)
        {
            var result = await _menuMongo.ReplaceOneAsync(x => x.Id == menu.Id, menu);
        }
    }
}
