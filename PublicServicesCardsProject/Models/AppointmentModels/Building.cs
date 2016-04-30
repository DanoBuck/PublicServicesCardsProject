using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    class Building
    {
        public int BuildingId { get; set; }

        // Buildings Have A Number Of Staff Working In Them
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
