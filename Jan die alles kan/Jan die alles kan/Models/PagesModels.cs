using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan.Models
{
  
   
    public class PagesContext : DbContext
    {
        public PagesContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<PagesContext>(null);
        }

        public DbSet<PagesModels> Pages { get; set; }
    }
    [Table("Pages")]
    public class PagesModels
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Permalink { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int Pageposition { get; set; }
        public string Seokey { get; set; }
        public string Seodiscription { get; set; }
    }   
}