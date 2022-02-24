using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Testing the robustness of the types used to contain parsed information.
            PerformCheck(ParsedTypeCompetency.TrueComparisons, "TrueComparisons");
            PerformCheck(ParsedTypeCompetency.FalseComparisons, "FalseComparisons");

            //Checking that the reflection parsing module works as expected.
            PerformCheck(ReflectionParsing.ClassParsing, "Reflection Class Parsing");
            Console.ReadKey();
        }

        static void PerformCheck(Action method, string name)
        {
            try
            {
                method();
                Console.WriteLine("{0} succeeded.", name);
            }
            catch(Exception err)
            {
                Console.WriteLine("{0} failed with message \"{1}\"", name, err.Message);
            }
        }
    }
}