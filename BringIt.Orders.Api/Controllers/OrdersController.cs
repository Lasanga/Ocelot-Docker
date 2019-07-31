using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BringIt.Auth.Api.Core;
using BringIt.Orders.Core.Models;
using BringIt.Orders.Core.Models.Dtos;
using BringIt.Orders.Services.OrderManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BringIt.Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IMapper _mapper;

        public OrdersController(IOrderManager orderManager, IMapper mapper)
        {
            _orderManager = orderManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddOrder")]
        [Authorize]
        public async Task AddOrder(OderInputDto input)
        {
            var order = new Order
            {
                CustomerId = input.CustomerId,
                DriverId = input.DriverId,
                ItemList = input.ItemList,
                Latitude = input.Latitude,
                Longitude = input.Longitude,
                RestaurantId = input.RestaurantId
            };
            await _orderManager.InsertAsync(order);
        }

        [HttpGet]
        [Route("GetAddedOrders")]
        [Authorize]
        public async Task<List<OrderOutputDto>> GetAddedOrders(int id)
        {
            var result = await _orderManager.GetByCustomerId(id);

            return result.Select(x => new OrderOutputDto
            {
                CustomerId = x.CustomerId,
                DriverId = x.DriverId,
                ItemList = x.ItemList,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                RestaurantId = x.RestaurantId,
                State = x.State,
                Id = x.Id
            }).ToList();
        }

        [HttpGet]
        [Route("GetPendingOrders")]
        [Authorize]
        public async Task<List<OrderOutputDto>> GetPendingOrders(double lat, double lng)
        {
            var result = await _orderManager.GetPendingByLatLong(lat, lng);

            return result.Select(x => new OrderOutputDto
            {
                CustomerId = x.CustomerId,
                DriverId = x.DriverId,
                ItemList = x.ItemList,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                RestaurantId = x.RestaurantId,
                State = x.State,
                Id = x.Id
            }).ToList();
        }

        [HttpPut]
        [Route("ConfirmOrder")]
        [Authorize(Roles =StaticRoleNames.Client)]
        public async Task ConfirmOrder(OrderUpdateDto input)
        {
            var order = await _orderManager.GetAsync(input.Id);
            await _orderManager.ConfirmCustomer(order);
        }

        [HttpPut]
        [Route("ConfirmDriver")]
        [Authorize(Roles = StaticRoleNames.Driver)]
        public async Task ConfirmDriver(OrderUpdateDto input)
        {
            var result = await _orderManager.GetAsync(input.Id);
            await _orderManager.ConfirmDriver(result, input.DriverId);
        }
    }
}