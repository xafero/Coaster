using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coaster.Roslyn
{
    public static class InputTool
    {
        public static SyntaxTree Parse(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);
            return tree;
        }

        public static StatementSyntax AsStatement(string code)
        {
            var syntax = SyntaxFactory.ParseStatement(code);
            return syntax;
        }

        public static bool HasErrors(SyntaxTree tree, out string[] errors)
        {
            var diagnostics = tree.GetDiagnostics().ToArray();
            if (diagnostics.Length >= 1)
            {
                errors = diagnostics.Select(d => d.ToString()).ToArray();
                return true;
            }
            errors = null;
            return false;
        }
    }
}