using Xunit;

namespace Coaster.Tests
{
    public class SourceTest2
    {
        [Fact]
        public void TestModify()
        {
            //var clazz = Coast.Parse<CSharpClassSource>("public class SomeClass {}");
            /*clazz.AddMethod()
                .SetPublic()
                .SetStatic(true)
                .SetName("main")
                .SetReturnTypeVoid()
                .SetBody("System.out.println(\"Hello World\");")
                .AddParameter("java.lang.String[]", "args");

            Console.WriteLine(clazz);*/

            /*
             *
             *Java Source Code Modification API
               ---------------------------------
               
               Of course it is possible to mix both approaches (parser and writer) to modify Java code programmatically:
               
               ```java
               JavaClassSource javaClass = 
                 Roaster.parse(JavaClassSource.class, "public class SomeClass {}");
               javaClass.addMethod()
                 .setPublic()
                 .setStatic(true)
                 .setName("main")
                 .setReturnTypeVoid()
                 .setBody("System.out.println(\"Hello World\");")
                 .addParameter("java.lang.String[]", "args");
               System.out.println(javaClass);
               ```
             *
             */
        }
    }
}