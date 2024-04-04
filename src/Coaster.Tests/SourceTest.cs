using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Coaster.Model;
using Coaster.Roslyn;
using Coaster.Utils;
using Xunit;

namespace Coaster.Tests
{
    public class SourceTest
    {
        [Fact]
        public void TestModify()
        {
            var unit = Coast.Parse("public class SomeClass {}");
            var clazz = unit.Members.Cast<CClass>().Single();

            clazz.Members.Add(new CMethod
            {
                Name = "Main"
            });

            WriteAndCompare(unit, $"{nameof(TestModify)}.txt");
        }

        [Fact]
        public void TestCreate()
        {
            var unit = new CUnit
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

            WriteAndCompare(unit, $"{nameof(TestCreate)}.txt");
        }

        private static void WriteAndCompare(CUnit unit, string name)
        {
            var code = unit.ToText();
            WriteText(name, code);
            var exp = ReadText($"res/{name}");

            var expN = TextTool.Normalize(exp);
            var codeN = TextTool.Normalize(code);
            Assert.Equal(expN, codeN);
        }

        private static string ReadText(string file)
        {
            return File.ReadAllText(file, Encoding.UTF8);
        }

        private static void WriteText(string file, string text)
        {
            File.WriteAllText(file, text, Encoding.UTF8);
        }
    }
}