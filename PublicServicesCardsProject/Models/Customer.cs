using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    class Customer : Person
    {
        public virtual string CivilStatus { get; set; }
    }
}
