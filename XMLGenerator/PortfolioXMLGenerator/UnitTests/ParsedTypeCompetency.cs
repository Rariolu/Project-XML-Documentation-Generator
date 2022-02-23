using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioGeneratorBackend;

namespace UnitTests
{
    public static class ParsedTypeCompetency
    {
        public static void TrueComparisons()
        {
            ParsedVariable pv1 = new ParsedVariable("pv", "String", "System", PROTECTION.PRIVATE);
            ParsedVariable pv2 = new ParsedVariable("pv", "String", "System", PROTECTION.PRIVATE);

            if (pv1 != pv2)
            {
                throw new Exception("ParsedVariable comparison failed.");
            }

            ParsedMethod pm1 = new ParsedMethod("pm", "Int32", "System", PROTECTION.PROTECTED);
            ParsedMethod pm2 = new ParsedMethod("pm", "Int32", "System", PROTECTION.PROTECTED);

            if (pm1 != pm2)
            {
                throw new Exception("ParsedMethod comparison failed.");
            }
        }
        public static void FalseComparisons()
        {
            ParsedVariable pv1 = new ParsedVariable("pv", "String", "System", PROTECTION.PRIVATE);
            ParsedVariable pv2 = new ParsedVariable("pv", "String", "System", PROTECTION.INTERNAL);

            if (pv1 == pv2)
            {
                throw new Exception("ParsedVariable comparison failed.");
            }
        }
    }
}