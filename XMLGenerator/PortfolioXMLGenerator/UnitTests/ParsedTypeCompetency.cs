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

            ParsedConstructor pc1 = new ParsedConstructor(PROTECTION.PUBLIC);
            ParsedConstructor pc2 = new ParsedConstructor(PROTECTION.PUBLIC);

            if (pc1 != pc2)
            {
                throw new Exception("ParsedConstructor comparison failed.");
            }

            ParsedProperty pp1 = new ParsedProperty("urgh", "Misc", "");
            ParsedProperty pp2 = new ParsedProperty("urgh", "Misc", "");

            if (pp1 != pp2)
            {
                throw new Exception("ParsedProperty comparison failed.");
            }

            ParsedType pt1 = new ParsedType("Misc", "System");
            pt1.AddVariable(pv1);
            pt1.AddMethod(pm1);
            pt1.AddMethod(pc1, true);
            pt1.AddProperty(pp1);

            ParsedType pt2 = new ParsedType("Misc", "System");
            pt2.AddVariable(pv2);
            pt2.AddMethod(pm2);
            pt2.AddMethod(pm2, true);
            pt2.AddProperty(pp2);

            if (pt1 != pt2)
            {
                throw new Exception("ParsedType comparison failed.");
            }

            ParsedAssembly pa1 = new ParsedAssembly("BleepBloop");
            pa1.AddType(pt1);

            ParsedAssembly pa2 = new ParsedAssembly("BleepBloop");
            pa2.AddType(pt2);

            if (pa1 != pa2)
            {
                throw new Exception("ParsedAssembly comparison failed.");
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

            ParsedMethod pm1 = new ParsedMethod("pm", "Int32", "System", PROTECTION.PROTECTED);
            ParsedMethod pm2 = new ParsedMethod("pm", "Int32", "System", PROTECTION.PUBLIC);

            if (pm1 == pm2)
            {
                throw new Exception("ParsedMethod comparison failed.");
            }

            ParsedConstructor pc1 = new ParsedConstructor(PROTECTION.PUBLIC);
            ParsedConstructor pc2 = new ParsedConstructor(PROTECTION.PRIVATE_PROTECTED);

            if (pc1 == pc2)
            {
                throw new Exception("ParsedConstructor comparison failed.");
            }

            ParsedProperty pp1 = new ParsedProperty("urgh", "Misc", "");
            ParsedProperty pp2 = new ParsedProperty("urgh", "Misc", "System");

            if (pp1 == pp2)
            {
                throw new Exception("ParsedProperty comparison failed.");
            }

            ParsedType pt1 = new ParsedType("Misc", "System");
            pt1.AddVariable(pv1);
            pt1.AddMethod(pm1);
            pt1.AddMethod(pc1, true);
            pt1.AddProperty(pp1);

            ParsedType pt2 = new ParsedType("Misc", "System");
            pt2.AddVariable(pv2);
            pt2.AddMethod(pm2);
            pt2.AddMethod(pm2, true);
            pt2.AddProperty(pp2);

            if (pt1 == pt2)
            {
                throw new Exception("ParsedType comparison failed.");
            }

            ParsedAssembly pa1 = new ParsedAssembly("BleepBloop");
            pa1.AddType(pt1);

            ParsedAssembly pa2 = new ParsedAssembly("BleepBloop");
            pa2.AddType(pt2);

            if (pa1 == pa2)
            {
                throw new Exception("ParsedAssembly comparison failed.");
            }
        }
    }
}