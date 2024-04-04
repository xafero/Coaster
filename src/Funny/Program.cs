namespace Funny
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Modify();
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
    }
}