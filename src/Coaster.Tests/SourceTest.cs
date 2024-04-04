using System.Linq;
using System.Runtime.Serialization;
using Coaster.API;
using Coaster.Model;
using Xunit;
using static Coaster.Tests.TestUtil;

namespace Coaster.Tests
{
    public class SourceTest
    {
        [Fact]
        public void TestFormat()
        {
            const string human = "public class MyClass{ private string field;}";
            var code = Coast.Format(human);

            WriteAndCompare(code, nameof(TestFormat));
        }

        [Fact]
        public void TestModify()
        {
            var unit = Coast.Parse("public class SomeClass {}");
            var clazz = unit.Members.Cast<CClass>().Single();

            clazz.Members.Add(new CMethod
            {
                Name = "Main"
            });

            WriteAndCompare(unit, nameof(TestModify));
        }

        [Fact]
        public void TestArgs()
        {
            var unit = new CUnit
            {
                Members =
                {
                    new CNamespace
                    {
                        Name = "Sample",
                        Members =
                        {
                            new CDelegate
                            {
                                Name = "Callback",
                                Params =
                                {
                                    new CParam { Type = "string", Name = "message" },
                                    new CParam { Type = "double", Name = "value" }
                                }
                            },
                            new CRecord
                            {
                                Name = "Person",
                                Params =
                                {
                                    new CParam { Type = "string", Name = "FirstName" },
                                    new CParam { Type = "string", Name = "LastName" }
                                }
                            },
                            new CStruct
                            {
                                Name = "Coords",
                                Members =
                                {
                                    new CMethod
                                    {
                                        Params =
                                        {
                                            new CParam { Type = "double", Name = "x" },
                                            new CParam { Type = "double", Name = "y" }
                                        },
                                        Body = new() { Statements = { "X = x", "Y = y" } }, IsConstructor = true
                                    },
                                    new CProperty { Type = "double", Name = "X", Mode = PropMode.Get },
                                    new CProperty { Type = "double", Name = "Y", Mode = PropMode.Get },
                                    new CMethod
                                    {
                                        Name = "ToString", Type = "string", IsOverride = true,
                                        Body = new() { Statements = { "$\"({X}, {Y})\"" }, IsArrow = true }
                                    }
                                }
                            },
                            new CInterface
                            {
                                Name = "IPoint",
                                Members =
                                {
                                    new CProperty { Type = "int", Name = "X" },
                                    new CProperty { Type = "int", Name = "Y" },
                                    new CProperty { Type = "double", Name = "Distance", Mode = PropMode.Get },
                                    new CMethod { Name = "SampleMethod" }
                                }
                            },
                            new CEnum
                            {
                                Name = "ErrorCode", Type = "ushort",
                                Values =
                                {
                                    new CEnumVal { Name = "None", Value = "0" },
                                    new CEnumVal { Name = "Unknown" },
                                    new CEnumVal { Name = "ConnectionLost", Value = "100" },
                                    new CEnumVal { Name = "OutlierReading", Value = "200" }
                                }
                            }
                        }
                    }
                }
            };

            WriteAndCompare(unit, nameof(TestArgs));
        }

        [Fact]
        public void TestCreate()
        {
            var unit = new CUnit
            {
                Usings = { "System.Runtime.Serialization" },
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
                                Interfaces = { typeof(IExtensibleDataObject).FullName! },
                                Members =
                                {
                                    new CField { Name = "serialVersionUID", Type = "long" },
                                    new CProperty { Type = "int", Name = "Id" },
                                    new CProperty { Type = "string", Name = "FirstName" },
                                    new CProperty { Type = "string", Name = "LastName" },
                                    new CProperty { Type = "ExtensionDataObject", Name = "ExtensionData" },
                                    new CMethod { Name = "SetIt" },
                                    new CEvent { Name = "WebOpened" }
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
                            },
                            new CDelegate
                            {
                                Name = "EventHandler"
                            }
                        }
                    }
                }
            };

            WriteAndCompare(unit, nameof(TestCreate));
        }

        [Fact]
        public void TestCon()
        {
            var unit = new CUnit
            {
                Usings = { "System" },
                Members =
                {
                    new CClass
                    {
                        Name = "TestClass",
                        Members =
                        {
                            new CMethod
                            {
                                Name = "Main", IsStatic = true,
                                Params = { new CParam { Type = "string[]", Name = "args" } },
                                Body = new() { Statements = { "Console.WriteLine(args.Length)" } }
                            }
                        }
                    }
                }
            };

            WriteAndCompare(unit, nameof(TestCon));
        }
    }
}