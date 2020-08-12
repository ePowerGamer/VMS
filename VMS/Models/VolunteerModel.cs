using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VMS.Models
{
    public class VolunteerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Center { get; set; }
        public string Interests { get; set; }
        public string Availability { get; set; } //How to handle this??
        public string Address { get; set; }  //How to handle this??
        public int PhoneNumber { get; set; }
        [Key]
        public string Email { get; set; }
        public string Education { get; set; }
        public string Licenses { get; set; }
        public string EMCname { get; set; }
        public int EMCphone { get; set; }
        public string EMCemail { get; set; }
        public string EMCaddress { get; set; }  //How to handle this??
        public bool DriversLicenseOnFile { get; set; }
        public bool SocialCardOnFile { get; set; }
        public string ApprovalStatus { get; set; }
    }

    public class VolunteerContext : DbContext
    {
        public VolunteerContext() : base("VMSdb")
        {

        }

        public DbSet<VolunteerModel> Volunteers { get; set; }
    }
}