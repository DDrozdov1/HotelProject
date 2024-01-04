using System;
using System.Collections.Generic;

namespace AppKursProject.Models
{
    public partial class ProvidedService
    {
        public int ServiceRecordId { get; set; }
        public int? StayId { get; set; }
        public int? ServiceId { get; set; }
        public int? EmployeeId { get; set; }
        public decimal? TotalCost { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual HotelService? Service { get; set; }
        public virtual Stay? Stay { get; set; }
    }
}
