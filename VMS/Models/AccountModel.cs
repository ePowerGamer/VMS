using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VMS.Models
{
    public class AccountModel
    {
        
        [Key]
        public string Username { get; set; }

        
        public string Password { get; set; }

        public string Salt { get; set; }
        public string Role { get; set; }
    }
    public class AccountContext : DbContext
    {
        public AccountContext() : base("VMSdb")
        {
            //Initialize with connection to VMS database
        }

        public DbSet<AccountModel> Accounts { get; set; }
    }

}