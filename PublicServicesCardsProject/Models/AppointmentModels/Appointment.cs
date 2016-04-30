using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfAppointment { get; set; }

        [DataType(DataType.Time)]
        public DateTime TimeOfAppointment { get; set; }

        // These Three Objects Make An Appointment Possible
        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Building Building { get; set; }
    }
}
