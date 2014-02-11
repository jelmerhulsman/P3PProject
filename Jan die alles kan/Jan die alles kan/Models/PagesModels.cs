using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan.Models
{
    public class PagesModels
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PagesContext : DbContext
    {
        public PagesContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<PagesModels> Pages { get; set; }
    }
}