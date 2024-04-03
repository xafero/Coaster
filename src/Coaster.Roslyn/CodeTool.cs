using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coaster.Roslyn
{
    public static class CodeTool
    {
        public static NameSyntax ToName(string text)
        {
            var syntax = SyntaxFactory.ParseName(text);
            return syntax;
        }
    }
}