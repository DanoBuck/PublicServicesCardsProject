using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    public class Staff : Person
    {
        public double Salary { get; set; }
        public int DeskNumber { get; set; }

        // Staff Have A Number Of Appointments Which Take Place In A Building
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual Building Building { get; set; }
    }
}
