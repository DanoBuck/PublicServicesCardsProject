using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PublicServicesCardsProject.Models
{
    public class Building
    {
        [Key]
        public int BuildingId { get; set; }
        [StringLength(200, ErrorMessage = "Safe Office cannot be longer than 200 characters.")]
        [Required]
        public string SafeOffice { get; set; }
        [StringLength(200, ErrorMessage = "Address Line 1 cannot be longer than 200 characters.")]
        [Required]
        public string AddressLine1 { get; set; }
        [StringLength(200, ErrorMessage = "Address Line 2 cannot be longer than 200 characters.")]
        public string AddressLine2 { get; set; }
        [StringLength(200, ErrorMessage = "Address Line 3 cannot be longer than 200 characters.")]
        public string AddressLine3 { get; set; }
        [StringLength(200, ErrorMessage = "Address Line 4 cannot be longer than 200 characters.")]
        public string AddressLine4 { get; set; }
        [Required]
        public string County { get; set; }
        [Required]
        public string Phone { get; set; }
        // Buildings Have A Number Of Staff Working In Them
        public virtual IQueryable<Staff> Staff { get; set; }

        public string Address
        {
            get
            {
                string address = " ";
                if (!(String.IsNullOrEmpty(AddressLine2)))
                {
                    if(String.IsNullOrEmpty(AddressLine3) && String.IsNullOrEmpty(AddressLine4))
                    {
                        address = AddressLine1 + ", " + AddressLine2 + ", " + County;
                    }
                    else if (AddressLine3 != null && AddressLine4 == null)
                    {
                        address = AddressLine1 + ", " + AddressLine2 + ", " + AddressLine3 + ", " + County;
                    }
                    else
                    {
                        address = AddressLine1 + ", " + AddressLine2 + ", " + AddressLine3 + ", " + AddressLine4 + ", " + County;
                    }
                }
                else if (!(String.IsNullOrEmpty(AddressLine3)))
                {
                    if (String.IsNullOrEmpty(AddressLine4))
                    {
                        address = AddressLine1 + ", " + AddressLine3 + ", " + County;
                    }
                    else if (AddressLine4 != null)
                    {
                        address = AddressLine1 + ", " + AddressLine3 + ", " + AddressLine4 + ", " + County;
                    }
                }
                else if (!(String.IsNullOrEmpty(AddressLine4)))
                {
                    address = AddressLine1 + ", " + AddressLine4 + ", " + County;
                }
                return address;
            }
        }
    }
}
