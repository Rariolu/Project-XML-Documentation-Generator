using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioXMLGenerator
{
    public static class Util
    {
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
    }
}