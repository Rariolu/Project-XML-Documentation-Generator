using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    public static class Util
    {
        public static string NormaliseString(this object obj)
        {
            return obj.ToString().ToLower().Trim();
        }

        public static string RemoveIndentsAndNewLines(this string str)
        {
            return str.Trim().Replace("\t", "").Replace("\n", "");
        }

        /// <summary>
        /// Remove invalid characters from a path string.
        /// Taken almost verbatim from a StackOverflow answer:
        /// https://stackoverflow.com/questions/146134/how-to-remove-illegal-characters-from-path-and-filenames
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string RemoveInvalidPathChars(this string originalString)
        {
            if (!string.IsNullOrEmpty(originalString))
            {
                return string.Concat(originalString.Split(Path.GetInvalidFileNameChars()));
            }
            return string.Empty;
        }

        /// <summary>
        /// Check a string to check if it contains a boolean value (i.e. "true" or "false") and sets the "val" parameter to the appropriate value if that is the case.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool StringContainsBool(this string str, out bool val)
        {
            str = str.NormaliseString();
            if (str == "true" || str == "false")
            {
                val = str == "true";
                return true;
            }
            val = false;
            return false;
        }

        /// <summary>
        /// Checks a given string to determine if it contains an enum value of a given type.
        /// </summary>
        /// <typeparam name="T">Desired enum type</typeparam>
        /// <param name="str">Text that is checked.</param>
        /// <param name="val">The outputted enum value.</param>
        /// <returns></returns>
        public static bool StringContainsEnum<T>(this string str, out T val) where T : struct
        {
            Type type = typeof(T);
            val = default(T);
            if (!type.IsEnum)
            {
                return false;
            }

            Array enumValues = Enum.GetValues(type);
            foreach (T enumVal in enumValues)
            {
                if (str.NormaliseString() == enumVal.NormaliseString())
                {
                    val = enumVal;
                    return true;
                }
            }

            return false;
        }
    }
}
