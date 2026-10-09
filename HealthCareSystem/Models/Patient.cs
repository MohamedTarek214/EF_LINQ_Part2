using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Models
{
    internal class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public List<Appointment> Appointments { get; set; } 
    }
}
