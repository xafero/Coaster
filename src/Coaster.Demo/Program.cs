using System;
using Coaster.Model.Part;
using Coaster.Model.Top;
using Coaster.Model.Tree;
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
                Members =
                {
                    new CNamespace
                    {
                        Name = "Example", Members =
                        {
                            new CClass
                            {
                                Name = "Sample", Members =
                                {
                                    new CConstructor
                                    {
                                        Body = new CArrow { Expression = "Console.WriteLine(\"Hello!\")" }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Console.WriteLine(unit.ToText());
        }
    }
}