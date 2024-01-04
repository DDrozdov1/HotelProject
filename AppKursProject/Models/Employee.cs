using System;
using System.Collections.Generic;

namespace AppKursProject.Models
{
        public partial class Employee
        {
            public Employee()
            {
                ProvidedServices = new HashSet<ProvidedService>();
                RoomServices = new HashSet<RoomService>();
            }

            public int EmployeeId { get; set; }
            public string? Name { get; set; }
            public string? Position { get; set; }

            public virtual ICollection<ProvidedService> ProvidedServices { get; set; }
            public virtual ICollection<RoomService> RoomServices { get; set; }
        }
}
