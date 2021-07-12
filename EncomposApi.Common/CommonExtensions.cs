using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EncomposApi
{
    /// <summary>
    /// Common extensions, no particular to a given domain or model
    /// </summary>
    public static class CommonExtensions
    {
        public static string MaxLength(this string input, int length)
        {
            if (input == null) return null;
            return input.Substring(0, Math.Min(length, input.Length));
        }

        /// <summary>
        /// Strip out non-numeric characters from a phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string NormalizePhone(this string phone) 
        {
            if (string.IsNullOrEmpty(phone)) return phone;
            return Regex.Replace(phone, "[^0-9]", "");
        }

        public static bool ContainsAnyKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params TKey[] keys)
        {
            foreach (var key in keys)
            {
                if (dictionary.ContainsKey(key)) return true;
            }
            return false;
        }


        public static Dictionary<string, object> Minus(this Dictionary<string, object> props1, Dictionary<string, object> props2)
        {
            if (props2 == null) return new Dictionary<string, object>(props1);

            var result = new Dictionary<string, object>();
            foreach (var (key1, value1) in props1)
            {
                if (props2.TryGetValue(key1, out object value2))
                {
                    if (value1 == null && value2 == null) continue;
                    if (value1 != null && value1.Equals(value2)) continue;
                }
                result[key1] = value1;
            }
            return result;
        }


        public static Dictionary<string, object> ToValues<T>(this T obj, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return obj.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(obj, null)
            );
        }

        public static T CopyFrom<T>(this T obj1, T obj2, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            if (obj2 == null) return obj1;

            Type type = typeof(T);
            foreach (var prop in type.GetProperties(bindingAttr))
            {
                object value = prop.GetValue(obj2, null);
                type.GetProperty(prop.Name).SetValue(obj1, value, null);
            }
            return obj1;
        }

        public static T ToObject<T>(this IDictionary<string, object> properties) where T : class, new()
        {
            var obj = new T();
            var rowType = obj.GetType();

            foreach (var item in properties)
            {
                rowType.GetProperty(item.Key).SetValue(obj, item.Value, null);
            }

            return obj;
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> @default = null)
        {
            if (dictionary.TryGetValue(key, out TValue value)) return value;
            if (@default == null) return default;
            return @default();
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue @default)
        {
            return dictionary.Get(key, () => @default);
        }

        public static bool IsLocalNetworkConnection(this HttpRequest request)
        {
            var remoteIp = request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            if (remoteIp == "0.0.0.1") return true;
            if (remoteIp == "127.0.0.1") return true;
            if (remoteIp.StartsWith("192.168.")) return true;
            return false;
        }

        public static DateTimeOffset? WithTime(this DateTime? date, TimeSpan? time) 
        {
            if (date == null) return null;
            if (time == null) return date;
            return date + time;
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
