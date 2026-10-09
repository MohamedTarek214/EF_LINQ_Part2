using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Models
{
    internal class Doctor
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Specialization { get; set; } 
        public List<Appointment> Appointments { get; set; } 
    }
}
