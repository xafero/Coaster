using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coaster.Roslyn
{
    public static class OutputTool
    {
        public static NameSyntax ToName(string text)
        {
            var syntax = SyntaxFactory.ParseName(text);
            return syntax;
        }

        public static string ToText(this SyntaxNode node)
        {
            var code = node.NormalizeWhitespace().ToFullString();
            return code;
        }

        public static BaseTypeSyntax ToBaseType(string name)
        {
            return SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(name));
        }

        public static ClassDeclarationSyntax ToSyntax(this Class cla)
        {
            var bases = cla.Interfaces.Select(ToBaseType).ToArray();
            var clas = SyntaxFactory.ClassDeclaration(cla.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(bases);
            return clas;
        }

        public static NamespaceDeclarationSyntax ToSyntax(this Namespace nsp)
        {
            var name = ToName(nsp.Name);
            var usings = nsp.Usings.Select(ToUsing).ToArray();
            var space = SyntaxFactory.NamespaceDeclaration(name)
                .AddUsings(usings)
                .AddMembers(nsp.Members.Select(ToSyntax).ToArray());
            return space;
        }

        public static MemberDeclarationSyntax ToSyntax(this Member member)
        {
            if (member is Class c)
            {
                return ToSyntax(c);
            }
            throw new InvalidOperationException($"{member} ?!");
        }

        public static UsingDirectiveSyntax ToUsing(string text)
        {
            var name = ToName(text);
            return SyntaxFactory.UsingDirective(name);
        }
    }
}