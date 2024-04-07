using System.IO;
using System.Text;
using Coaster.Model.Top;
using Coaster.Roslyn;
using Coaster.Utils;
using Xunit;

namespace Coaster.Tests
{
    internal static class TestUtil
    {
        internal static void WriteAndCompare(CUnit unit, string name)
        {
            var code = unit.ToText();
            WriteAndCompare(code, name);
        }

        internal static void WriteAndCompare(string code, string name)
        {
            name += ".cs";

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