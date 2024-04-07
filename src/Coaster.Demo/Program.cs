using System;
using Coaster.Model;
using Coaster.Roslyn;

namespace Coaster.Demo
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var unit = new CUnit
            {
                Usings = { "System", "System.Linq", "System.IO" },
                Members = { new CClass { Name = "Sample" } }
            };
            Console.WriteLine(unit.ToText());
        }
    }
}