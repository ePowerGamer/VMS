using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VMS.Models
{
    public class OpportunityModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Center { get; set; }
        public string Tags { get; set; }
        public string Days { get; set; }
        public string Time { get; set; }
    }

    public class OpportunityContext : DbContext
    {
        public OpportunityContext() : base("VMSdb")
        {

        }

        public DbSet<OpportunityModel> Opportunities { get; set; }

    }
}