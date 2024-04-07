using System;
using Coaster.API.Top;
using Coaster.Model;
using Coaster.Roslyn;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coaster
{
    public static class Coast
    {
        internal static CompilationUnitSyntax ParseUnit(string code)
        {
            var syntax = InputTool.Parse(code);
            var root = syntax.GetCompilationUnitRoot();
            return root;
        }

        public static CUnit Parse(string code)
        {
            var root = ParseUnit(code);
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

        private static IMember Go(MemberDeclarationSyntax syntax)
        {
            return syntax switch
            {
                ClassDeclarationSyntax c => new CClass { Name = c.Identifier.ToString() },
                _ => throw new InvalidOperationException($"{syntax} ?!")
            };
        }

        public static string Format(string code)
        {
            var unit = ParseUnit(code);
            var formatted = unit.ToText();
            return formatted;
        }
    }
}