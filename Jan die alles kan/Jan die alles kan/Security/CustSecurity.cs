using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jan_die_alles_kan
{
    public static class CustSecurity
    {
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