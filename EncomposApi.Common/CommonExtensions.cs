using System;
using System.Text.RegularExpressions;

namespace EncomposApi;

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
}
