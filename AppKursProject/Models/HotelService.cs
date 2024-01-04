using System;
using System.Collections.Generic;

namespace AppKursProject.Models
{
    public partial class HotelService
    {
        public HotelService()
        {
            ProvidedServices = new HashSet<ProvidedService>();
        }

        public int ServiceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<ProvidedService> ProvidedServices { get; set; }
    }
}
