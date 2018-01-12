using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public static class HelperDTO
    {
        public static string GetLanguagesByCode(string code)
        {
            string language = string.Empty;
            switch (code)
            {
                case "en":
                    language = "English";
                    break;
                case "es":
                    language = "Spanish";
                    break;
                case "cn":
                    language = "Chinese";
                    break;
                case "tl":
                    language = "Tagalog";
                    break;
                case "vi":
                    language = "Vietnamese";
                    break;
                case "fr":
                    language = "French";
                    break;
                case "ge":
                    language = "German";
                    break;
                case "kr":
                    language = "Korean";
                    break;
                case "ar":
                    language = "Arabic";
                    break;
                case "ru":
                    return "Russian";
                    break;
                case "it":
                    return "Italian";
                    break;
            }

            return language;
        }

        public static string ToDateFormat(this string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime temp = new DateTime();
                DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out temp);

                return temp.ToString("MMM d, yyyy");
            }
            else
            {
                return string.Empty;
            }

        }
        public static DateTime yyyyMMddToDateTime(this string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime temp = new DateTime();
                DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out temp);

                return temp;
            }
            else
            {
                return new DateTime(1, 1, 1);
            }

        }
    }
}
