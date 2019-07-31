using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Users.Core.Models.Dtos
{
    public class UserRegisterInputDto
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string VehicleNumber { get; set; }
    }
}
