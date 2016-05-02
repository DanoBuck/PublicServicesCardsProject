﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    public class Building
    {
        [Key]
        public int BuildingId { get; set; }
        public string SafeOffice { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        // Buildings Have A Number Of Staff Working In Them
        public virtual IQueryable<Staff> Staff { get; set; }
    }
}
