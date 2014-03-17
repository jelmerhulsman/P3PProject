using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jan_die_alles_kan.Models
{
   
        public class PicturesContext : DbContext
        {
            public PicturesContext()
                : base("DefaultConnection")
            {
                Database.SetInitializer<PicturesContext>(null);
            }

            public DbSet<PictureModel> Picture { get; set; }
        }
        public class UploadModel
        {
            public HttpPostedFileBase File { get; set; }
        }
        [Table("Pictures")]
        public class PictureModel
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public DateTime MTime { get; set; }
            public DateTime CTime { get; set; }
            public string Color { get; set; }
            public string Description { get; set; }
            public string File_name { get; set; }
            public decimal Price { get; set; }
        }
        public class CategoryContext : DbContext
        {
            public CategoryContext()
                : base("DefaultConnection")
            {
                Database.SetInitializer<CategoryContext>(null);
            }

            public DbSet<Category> Categories { get; set; }
        }

        [Table("Category")]
        public class Category
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            public string Name { get; set; }
        }



        
    }
