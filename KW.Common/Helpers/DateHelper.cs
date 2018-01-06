using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Common
{
    public class DateHelper
    {
        public static string ToStringDate(string data)
        {
            try
            {
                DateTime date = new DateTime();
                if (DateTime.TryParseExact(data, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date.ToString("yyyyMMdd");
                }
                else if (DateTime.TryParseExact(data, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date.ToString("yyyyMMdd");
                }
                else if (DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date.ToString("yyyyMMdd");
                }
                else
                {
                    date = DateTime.Parse(data);
                    return date.ToString("yyyyMMdd");
                }
            }
            catch (Exception ex)
            {
                data = "";
                return data;
            }
        }
        public static DateTime ParseDateFromYYMMDD(string date)
        {
            DateTime hireDate;
            try
            {
                DateTime dateCont = new DateTime();
                if (DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateCont))
                {
                    hireDate = dateCont;
                }
                else
                {
                    hireDate = DateTime.Parse(date);
                }
                return hireDate;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw new Exception(error);
            }
        }
        public static string DateStampNow()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        public static string TimeStampNow()
        {
            return DateTime.Now.ToString("HHmm");
        }

        public static DateTime StringToDateTime(string data)
        {
            DateTime date = new DateTime();
            try
            {

                if (DateTime.TryParseExact(data, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    date = DateTime.Parse(data);
                    return date;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid date format parameter");
            }
        }

        public static DateTime GetDateTime()
        {
            try
            {
                DateTime date = DateTime.Now;
                return date;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get current date");
            }
        }
    }
}
