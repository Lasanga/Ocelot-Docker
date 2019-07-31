using BringIt.Orders.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BringIt.Orders.Services.OrderManagers
{
    public interface IOrderManager
    {
        Task InsertAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(string id);
        Task<Order> GetAsync(string id);
        Task ConfirmDriver(Order order, int driverId);
        Task ConfirmCustomer(Order order);
        Task<List<Order>> GetByDriverId (int id);
        Task<List<Order>> GetByCustomerId(int id);
        Task<List<Order>> GetPendingByLatLong(double lat, double lng);
    }
}
