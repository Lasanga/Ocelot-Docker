using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BringIt.Restaurants.Core.Models
{
    public class FullAudited
    {
        [Key]
        public int Id { get; set; }

        public DateTime? CreationTime { get; set; }

        public int? CreatorId { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTime? DeletionTime { get; set; }
    }
}
