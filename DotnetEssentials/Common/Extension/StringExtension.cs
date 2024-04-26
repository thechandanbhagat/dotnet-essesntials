using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotnetEssentials.Common.Extension
{
    public static class StringExtension
    {
        public static string ToUpperFirstLetter(this string str)
        {
            return string.IsNullOrEmpty(str) ? str : char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string ToCamelCase(this string str)
        {
            return string.IsNullOrEmpty(str) || str.Length < 2 ? str.ToLowerInvariant() : char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string GetHashString(this string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(s.ToLower().Trim()))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static (string, int) MaxListLines(this string s, int maxLines = 5)
        {
            var arr = s.Split('\n');

            string addedVal = "";
            StringBuilder FinalStr = new StringBuilder();
            var maxlength = s.Length > 1000 ? 1000 : s.Length;
            var actualDelta = s.Length - maxlength;
            if (actualDelta > 0)
            {
                addedVal = $"\n... {actualDelta} more characters.";
            }
            var newArr = s.Substring(0, maxlength).Split('\n');
            if (newArr.Length > 5)
            {
                var arrLength = newArr.Length;
                Array.Resize(ref newArr, maxLines);
                string str = string.Join("\n", newArr);

                Array.Resize(ref newArr, maxLines + 1);
                newArr[maxLines] = $"\n... {arr.Length - maxLines} more lines {addedVal}";
                return (string.Join("\n", newArr), arr.Length);
            }
            else
            {
                return ($"{s.Substring(0, maxlength)} {addedVal}", arr.Length);
            }
        }
    }
}