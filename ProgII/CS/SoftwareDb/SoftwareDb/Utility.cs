using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoftwareDb
{
    class Utility
    {
        public static void ValidateDate(string s)
        {
            Regex r = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[0-2])\.(19[0-9]{2}|[2-9][0-9]{3})$");

            if (!r.IsMatch(s)) throw new ApplicationException("Date is not in correct format");
        }

        public static DateTime ParseDate(string s)
        {
            ValidateDate(s);
            //DateTime.Parse(s, System.Globalization.CultureInfo.InvariantCulture); // MM/DD/YYYY
            // DD.MM.YYYY
            string[] parts = s.Split('.');
            int day = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);

            return new DateTime(year, month, day);
        }

        // ("Very long string", 13) => "Very long ..."
        public static string TruncateString(string str, int maxlength)
        {
            return (str.Length <= maxlength) ? str : (str.Substring(0, maxlength - 3) + "...");
        }
    }
}
