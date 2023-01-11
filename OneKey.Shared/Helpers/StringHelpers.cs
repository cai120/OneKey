using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace OneKey.Shared.Helpers;

public static class StringHelper
{
    public static List<string> GenerateIncludes(List<string> includes)
    {
        var result = new List<string>();

        foreach (var include in includes)
        {
            var includeString = include.ToString();
            var index = includeString.IndexOf('.');

            includeString = includeString.Remove(0, index + 1);

            if (includeString.Contains(".First()"))
                includeString = includeString.Replace(".First()", "");

            result.Add(includeString);
        }

        return result;
    }
    
    public static List<string> GenerateIncludes<TEntity>(params Expression<Func<TEntity, object>>[] includes)
    {
        var result = new List<string>();
        
        foreach (var include in includes)
        {
            var includeString = include.ToString();
            var index = includeString.IndexOf('.');
        
            includeString = includeString.Remove(0, index + 1);
        
            if (includeString.Contains(".First()"))
                includeString = includeString.Replace(".First()", "");
        
            result.Add(includeString);
        }
        
        return result;
    }

    public static bool IsBase64String(this string base64)
    {
        base64 = base64.Trim();
        return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }

    public static string GenerateRandomCode(int length)
    {
        var random = new Random();
        
        const string chars = "012A0BC1DE2FG3HI4JK5789LM6NO7PQ8RS9TUVWXYZ3456";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string GenerateExpression(string expression)
    {
        expression = expression.Replace("Convert(", "");
        expression = expression.Replace(", Int32)", "");

        return expression;
    }
}
