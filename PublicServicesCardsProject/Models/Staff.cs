using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    class Staff : Person
    {
        public virtual double Salary { get; set; }
        public virtual int DeskNumber { get; set; }
    }
}
