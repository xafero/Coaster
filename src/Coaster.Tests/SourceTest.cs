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

            /*
             *
             *Java Source Code Generation API
               -------------------------------
               
               Roaster provides a fluent API to generate java classes. Here an example:
               
               ```java
               final JavaClassSource javaClass = Roaster.create(JavaClassSource.class);
               javaClass.setPackage("com.company.example").setName("Person");
               
               javaClass.addInterface(Serializable.class);
               javaClass.addField()
                 .setName("serialVersionUID")
                 .setType("long")
                 .setLiteralInitializer("1L")
                 .setPrivate()
                 .setStatic(true)
                 .setFinal(true);
               
               javaClass.addProperty(Integer.class, "id").setMutable(false);
               javaClass.addProperty(String.class, "firstName");
               javaClass.addProperty("String", "lastName");
               
               javaClass.addMethod()
                 .setConstructor(true)
                 .setPublic()
                 .setBody("this.id = id;")
                 .addParameter(Integer.class, "id");
               ```
               
               Will produce:
               
               ```java
               package com.company.example;
               
               import java.io.Serializable;
               
               public class Person implements Serializable {
               
                  private static final long serialVersionUID = 1L;
                  private final Integer id;
                  private String firstName;
                  private String lastName;
               
                  public Integer getId() {
                     return id;
                  }
               
                  public String getFirstName() {
                     return firstName;
                  }
               
                  public void setFirstName(String firstName) {
                     this.firstName = firstName;
                  }
               
                  public String getLastName() {
                     return lastName;
                  }
               
                  public void setLastName(String lastName) {
                     this.lastName = lastName;
                  }
               
                  public Person(Integer id) {
                     this.id = id;
                  }
               }
               ```
             *
             */
        }

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