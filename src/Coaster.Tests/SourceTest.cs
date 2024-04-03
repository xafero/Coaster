using Coaster.Impl;
using Xunit;

namespace Coaster.Tests
{
    public class SourceTest
    {
        [Fact]
        public void TestCreate()
        {
            // var clazz = Coast.Create<CSharpClassSource>();
            // clazz.SetNamespace("com.company.example").SetName("Person");

            /*
            clazz.AddInterface(typeof(ISerializable));
            clazz.AddField()
                .SetName("serialVersionUID")
                .SetType("long")
                .SetLiteralInitializer("1L")
                .SetPrivate()
                .SetStatic(true)
                .SetFinal(true);

            clazz.AddProperty(typeof(int), "id").SetMutable(false);
            clazz.AddProperty(typeof(string), "firstName");
            clazz.AddProperty("String", "lastName");

            clazz.AddMethod()
                .SetConstructor(true)
                .SetPublic()
                .SetBody("this.id = id;")
                .AddParameter(typeof(int), "id");
            */
        }

        [Fact]
        public void TestModify()
        {
            var clazz = Coast.Parse<CSharpClassSource>("public class SomeClass {}");
            /*clazz.AddMethod()
                .SetPublic()
                .SetStatic(true)
                .SetName("main")
                .SetReturnTypeVoid()
                .SetBody("System.out.println(\"Hello World\");")
                .AddParameter("java.lang.String[]", "args");

            Console.WriteLine(clazz);*/
        }
    }
}