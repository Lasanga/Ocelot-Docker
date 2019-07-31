using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Users.Core.Models.Dtos
{
    public class UserOutputDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public string VehicleNumber { get; set; }
    }
}
