﻿using System.Text.RegularExpressions;

namespace Common
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Reduces the length of the <paramref name="input"/> and appends the <paramref name="finalizer"/> to humanize the returned string.
        /// </summary>
        /// <remarks>
        ///     Returns the string unchanged if the length is less or equal to <paramref name="maxLength"/>.
        /// </remarks>
        /// <param name="input">The input string to reduce the length of.</param>
        /// <param name="maxLength">The max length the input string is allowed to be.</param>
        /// <param name="killAtWhitespace">Wether to kill the string at whitespace instead of cutting off at a word.</param>
        /// <param name="finalizer">The finalizer to humanize this string with.</param>
        /// <returns>The input string reduced to fit the length set by <paramref name="maxLength"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the length of maxlength is below 0 after the finalizer has been reduced from it.</exception>
        public static string Reduce(this string input, int maxLength, bool killAtWhitespace = false, string finalizer = "...")
        {
            if (input is null)
                return string.Empty + finalizer;

            if (input.Length > maxLength)
            {
                maxLength -= (finalizer.Length + 1); // reduce the length of the finalizer + a single integer to convert to valid range.

                if (maxLength < 1)
                    throw new ArgumentOutOfRangeException(nameof(maxLength));

                if (killAtWhitespace)
                {
                    var range = input.Split(' ');
                    for (int i = 2; input.Length + finalizer.Length > maxLength; i++) // set i as 2, 1 for index reduction, 1 for initial word removal, then increment.
                        input = string.Join(' ', range[..(range.Length - i)]);

                    input += finalizer;
                }
                return input[..maxLength] + finalizer;
            }
            else return input;
        }

        /// <summary>
        ///     Check if a string is alphanumeric.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string str)
            => Regex.IsMatch(str.Replace(" ", ""), "^[a-zA-Z0-9]+$");

        /// <summary>
        ///     Check if a string contains the followed text in insensitive format.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="findText"></param>
        /// <returns></returns>
        public static bool ContainsInsensitive(this string str, string findText)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(findText))
                return false;

            return str.Contains(findText, StringComparison.OrdinalIgnoreCase);
        }
    }
}
