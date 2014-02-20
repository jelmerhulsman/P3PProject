using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan.Models
{


    public class UploadDownloadContext : DbContext
    {
        public UploadDownloadContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<UploadDownloadContext>(null);
        }

        public DbSet<UploadDownloadModel> Pictures { get; set; }
    }

    [Table("Pictures")]
    public class UploadDownloadModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public string MainCategorie { get; set; }
        public DateTime MTime { get; set; }
        public DateTime CTime { get; set; }
        public int Size { get; set; }
    }
}