using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan.Models
{
    public class CartContext : DbContext
    {
        public CartContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<PagesContext>(null);
        }

        public DbSet<CartModels> Cart { get;set;}
    }

    [Table("Cart")]
    public class CartModels
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Items { get; set; }
    }
    }