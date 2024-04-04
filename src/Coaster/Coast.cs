using System;
using Coaster.Model;
using Coaster.Roslyn;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coaster
{
    public static class Coast
    {
        public static CUnit Parse(string code)
        {
            var syntax = InputTool.Parse(code);
            var root = syntax.GetCompilationUnitRoot();
            var unit = Go(root);
            return unit;
        }

        private static CUnit Go(CompilationUnitSyntax syntax)
        {
            var unit = new CUnit();
            foreach (var member in syntax.Members)
                unit.Members.Add(Go(member));
            return unit;
        }

        private static CMember Go(MemberDeclarationSyntax syntax)
        {
            return syntax switch
            {
                ClassDeclarationSyntax c => new CClass { Name = c.Identifier.ToString() },
                _ => throw new InvalidOperationException($"{syntax} ?!")
            };
        }

        public static string Format(string code)
        {
            var unit = Parse(code);
            var formatted = unit.ToText();
            return formatted;
        }
    }
}