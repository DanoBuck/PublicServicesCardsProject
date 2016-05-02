using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public string PPSN { get; set; }
        public string CivilStatus { get; set; }

        public string Name { get { return FirstName + " " + LastName; } }

        public int Age
        {
            get
            {
                return (FindAge(DateTime.Today, DateOfBirth));
            }
        }

        public int FindAge(DateTime dateTime, DateTime dateOfBirth)
        {
            int today = DateTime.Today.Year;
            int birthday = dateOfBirth.Year;

            int age = today - birthday;
            // Posibilty Of This Not Working On A Leap Year
            if (dateTime.Month < dateOfBirth.Month || (dateTime.Month == dateOfBirth.Month && dateTime.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age;
        }
    }
}
