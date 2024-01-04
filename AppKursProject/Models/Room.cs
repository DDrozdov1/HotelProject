using System;
using System.Collections.Generic;

namespace AppKursProject.Models
{
    public partial class Room
    {
        public Room()
        {
            RoomPrices = new HashSet<RoomPrice>();
            RoomServices = new HashSet<RoomService>();
            Stays = new HashSet<Stay>();
        }

        public int RoomId { get; set; }
        public string? Type { get; set; }
        public int? Capacity { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<RoomPrice> RoomPrices { get; set; }
        public virtual ICollection<RoomService> RoomServices { get; set; }
        public virtual ICollection<Stay> Stays { get; set; }
    }
}
