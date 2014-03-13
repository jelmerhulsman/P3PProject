using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan
{
    public class CustSecurityContext : DbContext
    {
        public CustSecurityContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<CustSecurityContext>(null);
        }

        public DbSet<IPProfile> IPProfiles { get; set; }
    }

    public class IPVerificationContext : DbContext
    {
        public IPVerificationContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<IPVerificationContext>(null);
        }

        public DbSet<IPVerificationModel> IPVerificationEntries { get; set; }
    }

    [Table("IPVerification")]
    public class IPVerificationModel
    {
        public IPVerificationModel()
        { }
        public IPVerificationModel(string username, string ip, string code)
        {

            Username = username;
            IP = ip;
            Code = code;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string IP { get; set; }
        public string Code { get; set; }
    }

    [Table("AdminIPs")]
    public class IPProfile
    {
        public IPProfile()
        { }
        public IPProfile(string username, string ip)
        {

            Username = username;
            IP = ip;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string IP { get; set; }
    }

}