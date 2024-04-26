using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotnetEssentials.Common.Extension
{
    public static class EnumExtensions
    {
        private static readonly Regex CapitalLetterRegex = new Regex("([A-Z])");

        public static string GetDescription(this Enum enumValue) => CapitalLetterRegex
            .Replace(enumValue.ToString(), " $1")
            .TrimStart()
            .ToLower()
            .ToUpperFirstLetter();

        public static string GetDisplayName(this Enum enumValue)
        {
            var attribute = enumValue.GetAttributeOfType<DisplayAttribute>();
            return attribute?.Name ?? enumValue.ToString();
        }

        public static T GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString()).First();
            var attributes = memInfo.GetCustomAttributes(false).Where(a => a is T).Cast<T>().ToArray();
            return attributes.FirstOrDefault();
        }
    }
}