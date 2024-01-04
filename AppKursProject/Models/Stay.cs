using System;
using System.Collections.Generic;

namespace AppKursProject.Models
{
    public partial class Stay
    {
        public Stay()
        {
            ProvidedServices = new HashSet<ProvidedService>();
        }

        public int StayId { get; set; }
        public int? CustomerId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<ProvidedService> ProvidedServices { get; set; }
    }
}
