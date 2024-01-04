using System;
using System.Collections.Generic;

namespace AppKursProject.Models
{
    public partial class RoomPrice
    {
        public int PriceId { get; set; }
        public decimal? Price { get; set; }
        public int? RoomId { get; set; }

        public virtual Room? Room { get; set; }
    }
}
