using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan.Models
{
    public class UserDataContext : DbContext
    {
        public UserDataContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<UserDataContext>(null);
        }

        public DbSet<UserData> DBUserData { get; set; }
    }

    [Table("UserData")]
    public class UserData
    {

        public UserData(string username, string email, string street, string housenumber, string city, string postalcode)
        {
            Username = username;
            Email = email;
            Street = street;
            HouseNumber = housenumber;
            City = city;
            PostalCode = postalcode;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

    }
}