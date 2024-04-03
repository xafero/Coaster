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
            var nsp = new Namespace
            {
                Usings = { "System.Linq", "System", "System.IO" },
                Name = "Example",
                Members =
                {
                    new Class
                    {
                        Name = "Person",
                        Interfaces = { typeof(ISerializable).FullName! },
                        Members =
                        {
                            new Field { Name = "serialVersionUID", Type = "long" },
                            new Property { Type = "int", Name = "Id" },
                            new Property { Type = "string", Name = "FirstName" },
                            new Property { Type = "string", Name = "LastName" },
                            new Method { Name = "SetIt" }
                        }
                    }
                }
            };

            Console.WriteLine(nsp.ToSyntax().ToText());
        }
    }
}