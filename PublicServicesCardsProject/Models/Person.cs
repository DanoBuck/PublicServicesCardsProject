using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    class Person
    {
        public virtual int PersonId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string EmailAddress { get; set; }
    }
}
