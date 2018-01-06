using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Common.Validation
{
    public static class Validate
    {
        public static void IsNotNullOrWhiteSpace(string theObj, string msg, params object[] objs)
        {
            if (string.IsNullOrWhiteSpace(theObj))
            {
                if (string.IsNullOrWhiteSpace(msg))
                {
                    msg = "{0} can't be null or white space";
                }
                throw new Exception(string.Format(msg, objs));
            }
        }

        public static void IsNotNullOrWhiteSpace(string theObj)
        {
            IsNotNullOrWhiteSpace(theObj, string.Empty);
        }

        public static void NotNull(object theObj, string msg, params object[] objs)
        {
            if (theObj == null)
            {
                if (string.IsNullOrWhiteSpace(msg))
                {
                    msg = "{0} instance can't be null ";
                }
                throw new Exception(string.Format(msg, objs));
            }
        }

        public static void NotNull(object theObj)
        {
            NotNull(theObj, string.Empty);
        }

        public static void IsEmail(string email)
        {
            RegexUtilities util = new RegexUtilities();
            if (!util.IsValidEmail(email))
                throw new Exception("Invalid email format");
        }

        public static void IsPasswordSecure(string password)
        {
            RegexUtilities util = new RegexUtilities();
            if (!util.IsPasswordStrong(password))
                throw new Exception("Your password is not strong enough. Min 8 char, 1 Upper case, 1 Lower case, 1 digit, 1 special character.");
        }
        public static void IsOldPasswordValid(string currentPasword, string oldPassword)
        {
            if (currentPasword != oldPassword)
                throw new Exception("Incorrect old password.");
        }
        public static void IsValidSubDomainName(string subdomain)
        {
            RegexUtilities util = new RegexUtilities();
            if (!util.IsValidDomainName(subdomain))
                throw new Exception("Invalid subdomain name format");
        }

        public static void IsValidIP(string ip)
        {
            RegexUtilities util = new RegexUtilities();
            if (!IsIPv4(ip))
                throw new Exception("Invalid IP Address");
        }
        private static bool IsIPv4(string value)
        {
            var quads = value.Split('.');

            // if we do not have 4 quads, return false
            if (!(quads.Length == 4)) return false;

            // for each quad
            foreach (var quad in quads)
            {
                int q;
                // if parse fails
                // or length of parsed int != length of quad string (i.e.; '1' vs '001')
                // or parsed int < 0
                // or parsed int > 255
                // return false
                if (!Int32.TryParse(quad, out q)
                    || !q.ToString().Length.Equals(quad.Length)
                    || q < 0
                    || q > 255)
                { return false; }
            }

            return true;
        }
    }
}