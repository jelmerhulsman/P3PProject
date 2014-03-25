using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jan_die_alles_kan.Shortcodes
{
    public class ShortcodeContext : DbContext
    {
        public ShortcodeContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<ShortcodeContext>(null);
        }

        public DbSet<ShortAG> ShortAGCodes { get; set; }
        public DbSet<ShortHN> ShortHNCodes { get; set; }
        public DbSet<ShortOT> ShortOTCodes { get; set; }
        public DbSet<ShortUZ> ShortUZCodes { get; set; }
    }

    [Table("SC(A-G)")]
    public class ShortAG
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string Name { get; set; }
        public string Code { get; set; }
    }

    [Table("SC(H-N)")]
    public class ShortHN
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string Name { get; set; }
        public string Code { get; set; }
    }

    [Table("SC(O-T)")]
    public class ShortOT
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string Name { get; set; }
        public string Code { get; set; }
    }

    [Table("SC(U-Z)")]
    public class ShortUZ
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public static class ShortCheck
    {
        static char[] AtilG = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
        static char[] HtilN = { 'H', 'I', 'J', 'K', 'L', 'M', 'N' };
        static char[] OtilT = { 'O', 'P', 'Q', 'R', 'S', 'T' };
        static char[] UtilZ = { 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// This functions checks if a shortcode which was found is correct and returns the string the code should be changed into
        /// </summary>
        /// <param name="text">The shortcode found by the program</param>
        /// <returns>The text the shortcode stands for</returns>
        public static string ReturnShortText(string text)
        {
            if (AtilG.Contains<char>(text[0]))
            {
                ShortcodeContext db = new ShortcodeContext();

                ShortAG[] checks = db.ShortAGCodes.ToArray();

                for (int i = 0; i < checks.Length; i++)
                {
                    if (checks[i].Name == text)
                    {
                        return checks[i].Code;
                    }
                }
            }
            else if (HtilN.Contains<char>(text[0]))
            {
                ShortcodeContext db = new ShortcodeContext();

                ShortHN[] checks = db.ShortHNCodes.ToArray();

                for (int i = 0; i < checks.Length; i++)
                {
                    if (checks[i].Name == text)
                    {
                        return checks[i].Code;
                    }
                }
            }
            else if (OtilT.Contains<char>(text[0]))
            {
                ShortcodeContext db = new ShortcodeContext();

                ShortOT[] checks = db.ShortOTCodes.ToArray();

                for (int i = 0; i < checks.Length; i++)
                {
                    if (checks[i].Name == text)
                    {
                        return checks[i].Code;
                    }
                }
            }
            else if (UtilZ.Contains<char>(text[0]))
            {
                ShortcodeContext db = new ShortcodeContext();

                ShortUZ[] checks = db.ShortUZCodes.ToArray();

                for (int i = 0; i < checks.Length; i++)
                {
                    if (checks[i].Name == text)
                    {
                        return checks[i].Code;
                    }
                }
            }

            return "[ShortCode Error]";
        }
    }
}