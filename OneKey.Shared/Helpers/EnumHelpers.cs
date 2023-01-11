using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace OneKey.Shared.Helpers
{
    public static class EnumHelpers
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            string displayName = "";
            displayName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();

            if (string.IsNullOrWhiteSpace(displayName))
                displayName = enumValue.ToString();

            return displayName;
        }   
    }
}