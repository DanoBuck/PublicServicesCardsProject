using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int BuildingId { get; set; }
        public int StaffId { get; set; }
        public int CustomerId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateOfAppointment { get; set; }

        [Required]
        public string TimeOfAppointment { get; set; }

        // These Three Objects Make An Appointment Possible
        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Building Building { get; set; }
    }
}
