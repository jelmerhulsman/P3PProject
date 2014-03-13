using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan.Models
{
    public class CatogeryContext : DbContext
    {
        public CatogeryContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<CatogeryContext>(null);
        }

        public DbSet<Catogery> Catogeries { get; set; }
    }

    [Table("Categorie")]
    public class Catogery
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}