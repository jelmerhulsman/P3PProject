using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jan_die_alles_kan.Models
{
    public class UploadModel
    {
        public class PicturesContext : DbContext
        {
            public PicturesContext()
                : base("DefaultConnection")
            {
                Database.SetInitializer<PicturesContext>(null);
            }

            public DbSet<UploadModel> Pages { get; set; }
        }
        public HttpPostedFileBase File { get; set; }
        
        [Table("Pictures")]
        public class PagesModels
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Route { get; set; }
            public string MainCategorie { get; set; }
            public DateTime MTime { get; set; }
            public DateTime CTime { get; set; }
            public double Size { get; set; }
        }   



        
    }
}