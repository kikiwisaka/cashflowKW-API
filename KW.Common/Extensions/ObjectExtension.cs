﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Common.Extensions
{
    public static class ObjectExtension
    {
        public static int ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }
        public static bool IsEmpty(this Guid guid)
        {
            return guid.Equals(Guid.Empty);
        }
        public static bool IsNull(this object obj)
        {
            return ReferenceEquals(obj, null);
        }
        public static bool IsNotNull(this object obj)
        {
            return !ReferenceEquals(obj, null);
        }
        public static bool IsNotNullAndEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }
        public static bool IsNotNullAndWhiteSpace(this string str)
        {
            return !str.IsNullOrWhiteSpace();
        }
        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }
        public static bool IsNotEmpty(this IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                return true;
            }
            return false;
        }
        public static bool IsEmpty(this IEnumerable enumerable)
        {
            return !enumerable.IsNotEmpty();
        }

        /// <summary>
        /// Convert To Begin Day. example : (05/05/2013 00:00:00)
        /// </summary>
        public static DateTime ToBeginDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        /// <summary>
        /// Convert To End Day. example : (05/05/2013 23:59:59)
        /// </summary>
        public static DateTime ToEndDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        public static bool NotEquals(this string value, string comparer)
        {
            return !value.Equals(comparer);
        }

        public static bool NotEquals(this int value, string comparer)
        {
            return !value.Equals(comparer);
        }

        public static bool NotEquals(this double value, string comparer)
        {
            return !value.Equals(comparer);
        }

        public static bool Contains<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Count(predicate) > 0;
        }

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }

        public static bool IsZero(this int value)
        {
            return value == 0;
        }

        public static string ToJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
