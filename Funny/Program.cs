using System;
using System.Runtime.Serialization;
using Coaster;
using Coaster.Roslyn;

namespace Funny
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Modify();
            Create();
        }

        private static void Modify()
        {
            /*
            dynamic javaClass = Roaster.parse(typeof(JavaClassSource), "public class SomeClass {}");
            javaClass.addMethod()
                .setPublic()
                .setStatic(true)
                .setName("main")
                .setReturnTypeVoid()
                .setBody("System.out.println(\"Hello World\");")
                .addParameter("java.lang.String[]", "args");

            Console.WriteLine(javaClass);
            */
        }

        private static void Create()
        {
            var nsp = new CUnit
            {
                Usings = { "System.Linq", "System", "System.IO" },
                Members =
                {
                    new CNamespace
                    {
                        Name = "Example",
                        Members =
                        {
                            new CClass
                            {
                                Name = "Person",
                                Interfaces = { typeof(ISerializable).FullName! },
                                Members =
                                {
                                    new CField { Name = "serialVersionUID", Type = "long" },
                                    new CProperty { Type = "int", Name = "Id" },
                                    new CProperty { Type = "string", Name = "FirstName" },
                                    new CProperty { Type = "string", Name = "LastName" },
                                    new CMethod { Name = "SetIt" }
                                }
                            },
                            new CEnum
                            {
                                Name = "Funny"
                            },
                            new CInterface
                            {
                                Name = "IConductor"
                            },
                            new CStruct
                            {
                                Name = "Half"
                            },
                            new CRecord
                            {
                                Name = "DailyTemperature"
                            }
                        }
                    }
                }
            };

            Console.WriteLine(nsp.ToSyntax().ToText());
        }
    }
}