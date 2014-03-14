using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan.Models
{
    public class CategoryContext : DbContext
    {
        public CategoryContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<CategoryContext>(null);
        }

        public DbSet<Category> Categories { get; set; }
    }

    [Table("Categorie")]
    public class Category
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}