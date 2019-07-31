using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BringIt.Restaurants.Core.Models
{
    public class Restaurant : FullAudited
    {
        [Required]
        public decimal Latitutde { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public string City { get; set; }
    }
}
