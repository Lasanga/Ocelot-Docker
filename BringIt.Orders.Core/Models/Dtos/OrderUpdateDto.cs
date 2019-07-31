using BringIt.Orders.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Orders.Core.Models.Dtos
{
    public class OrderUpdateDto
    {
        public string Id { get; set; }
        public int DriverId { get; set; }
    }
}
