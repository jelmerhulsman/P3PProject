using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jan_die_alles_kan
{
    public static class CustSecurity
    {
        /// <summary>
        /// This function checks if an IP is part of an array with IP's
        /// </summary>
        /// <param name="IPArray">The array with IP's you want to use for checking</param>
        /// <param name="IP">The IP you want to check</param>
        /// <returns>A boolean value which indicates if the IP array contains the IP or not</returns>
        public static bool IPCheck(string[] IPArray, string IP)
        {
            for (int i = 0; i < IPArray.Length; i++)
            {
                if (IPArray[i] == IP)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if two IP's are the same
        /// </summary>
        /// <param name="IPCert">The certified IP</param>
        /// <param name="IP">The IP you want to check</param>
        /// <returns>A boolean value which indicates if the IP is verified or not</returns>
        public static bool IPCheck(string IPCert, string IP)
        {
            if (IPCert == IP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}