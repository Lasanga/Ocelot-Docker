using AutoMapper;
using BringIt.Orders.Core.Models;
using BringIt.Orders.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Orders.Core.OrderProfiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OderInputDto, Order>();
            CreateMap<Order, OrderOutputDto>();
            CreateMap<OrderUpdateDto, Order>();
        }
    }
}
