using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Xyaneon.Bioinformatics.FASTA.Extensions
{
    internal static class StringExtensions
    {
        private static readonly Regex WhitespaceRegex = new Regex(@"\s+");

        public static string RemoveAllWhitespace(this string str)
        {
            return WhitespaceRegex.Replace(str, "");
        }

        public static IEnumerable<string> SplitBy(this string str, int length)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, "The length cannot be less than one.");
            }

            for (int i = 0; i < str.Length; i += length)
            {
                if (length + i > str.Length)
                {
                    length = str.Length - i;
                }

                yield return str.Substring(i, length);
            }
        }
    }
}
