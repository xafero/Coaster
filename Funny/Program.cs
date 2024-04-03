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
                        Interfaces = { typeof(ISerializable).FullName! }
                    }
                }
            };
            
            Console.WriteLine(nsp.ToSyntax().ToText());

            /*            

            
            ((dynamic)javaClass.addField())
                .setName("serialVersionUID")
                .setType("long")
                .setLiteralInitializer("1L")
                .setPrivate()
                .setStatic(true)
                .setFinal(true);

            javaClass.addProperty(typeof(Integer), "id").setMutable(false);
            javaClass.addProperty(typeof(String), "firstName");
            javaClass.addProperty("String", "lastName");

            ((dynamic)javaClass.addMethod())
                .setConstructor(true)
                .setPublic()
                .setBody("this.id = id;")
                .addParameter(typeof(Integer), "id");

            Console.WriteLine(javaClass);
            */
        }
    }
}