using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BringIt.Orders.Core.Models;
using MongoDB.Driver;

namespace BringIt.Orders.Services.OrderManagers
{
    public class OrderManager : IOrderManager
    {
        private readonly IMongoCollection<Order> _orderMongo;

        public OrderManager()
        {
            //var client = new MongoClient("mongodb://localhost:27017");
            var client = new MongoClient("mongodb://lasanga:lasanga%40123@cluster0-shard-00-00-9yhhw.azure.mongodb.net:27017,cluster0-shard-00-01-9yhhw.azure.mongodb.net:27017,cluster0-shard-00-02-9yhhw.azure.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true");
            var database = client.GetDatabase("BringItDb");

            _orderMongo = database.GetCollection<Order>("OrderMongo");
        }

        public async Task ConfirmCustomer(Order order)
        {
            order.State = Core.Enums.OrderState.pending;
            await UpdateAsync(order);
        }

        public async Task ConfirmDriver(Order order, int driverId)
        {
            order.DriverId = driverId;
            order.State = Core.Enums.OrderState.confirmed;
            await UpdateAsync(order);
        }

        public async Task DeleteAsync(string id)
        {
            await _orderMongo.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Order> GetAsync(string id)
        {
            var result = await _orderMongo.FindAsync(x => x.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<List<Order>> GetByCustomerId(int id)
        {
            var result = await _orderMongo.FindAsync(x => x.CustomerId == id && x.State == Core.Enums.OrderState.added);
            return result.ToList();
        }

        public async Task<List<Order>> GetByDriverId(int id)
        {
            var result = await _orderMongo.FindAsync(x => x.DriverId == id && x.State == Core.Enums.OrderState.pending);
            return result.ToList();
        }

        public async Task<List<Order>> GetPendingByLatLong(double lat, double lng)
        {
            var result = await _orderMongo.FindAsync(x => true);
            //var result = await _orderMongo.FindAsync(x => x.State == Core.Enums.OrderState.pending && x.Latitude == lat && x.Longitude == lat);
            return result.ToList().Where(x => x.DriverId == 0 && x.State == Core.Enums.OrderState.pending).ToList();
        }

        public async Task InsertAsync(Order order)
        {
            order.State = Core.Enums.OrderState.added;
            await _orderMongo.InsertOneAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            var result = await _orderMongo.ReplaceOneAsync(x => x.Id == order.Id, order);
        }


    }
}
