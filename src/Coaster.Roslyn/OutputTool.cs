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
                .AddBaseListTypes(bases)
                .AddMembers(cla.Members.Select(ToSyntax).ToArray());
            return clas;
        }

        public static MethodDeclarationSyntax ToSyntax(this Method met)
        {
            var method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(met.Type), met.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block());
            return method;
        }

        public static FieldDeclarationSyntax ToSyntax(this Field fld)
        {
            var variable = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(fld.Type))
                .AddVariables(SyntaxFactory.VariableDeclarator(fld.Name));
            var field = SyntaxFactory.FieldDeclaration(variable)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
            return field;
        }

        public static PropertyDeclarationSyntax ToSyntax(this Property prop)
        {
            var get = SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var set = SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var property = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(prop.Type), prop.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(get, set);
            return property;
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