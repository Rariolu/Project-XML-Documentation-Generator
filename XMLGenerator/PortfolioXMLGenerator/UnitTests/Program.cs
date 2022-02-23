using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformCheck(ParsedTypeCompetency.TrueComparisons, "TrueComparisons");
            PerformCheck(ParsedTypeCompetency.FalseComparisons, "FalseComparisons");
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