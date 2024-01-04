using System;
using System.Collections.Generic;

namespace AppKursProject.Models
{
    public partial class RoomService
    {
        public int ServiceId { get; set; }
        public int? RoomId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Room? Room { get; set; }
    }
}
