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

        public static BaseTypeSyntax[] ToBaseTypes(this IHasInterfaces cla)
        {
            var bases = cla.Interfaces.Select(ToBaseType).ToArray();
            return bases.Length == 0 ? null : bases;
        }

        public static ClassDeclarationSyntax ToSyntax(this CClass cla)
        {
            var clas = SyntaxFactory.ClassDeclaration(cla.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(cla.Members.Select(ToSyntax).ToArray());
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            return clas;
        }

        public static RecordDeclarationSyntax ToSyntax(this CRecord cla)
        {
            var rec = SyntaxFactory.Token(SyntaxKind.RecordKeyword);
            var clas = SyntaxFactory.RecordDeclaration(rec, cla.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            clas = clas.WithParameterList(SyntaxFactory.ParameterList());
            clas = clas.WithSemicolonToken(GetSemi());
            return clas;
        }

        public static DelegateDeclarationSyntax ToSyntax(this CDelegate cla)
        {
            var rt = SyntaxFactory.ParseTypeName(cla.Type);
            var clas = SyntaxFactory.DelegateDeclaration(rt, cla.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            clas = clas.WithParameterList(SyntaxFactory.ParameterList());
            clas = clas.WithSemicolonToken(GetSemi());
            return clas;
        }

        public static SyntaxToken GetSemi()
        {
            return SyntaxFactory.Token(SyntaxKind.SemicolonToken);
        }

        public static StructDeclarationSyntax ToSyntax(this CStruct cla)
        {
            var clas = SyntaxFactory.StructDeclaration(cla.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(cla.Members.Select(ToSyntax).ToArray());
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            return clas;
        }

        public static EnumDeclarationSyntax ToSyntax(this CEnum enu)
        {
            var ed = SyntaxFactory.EnumDeclaration(enu.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            return ed;
        }

        public static InterfaceDeclarationSyntax ToSyntax(this CInterface cla)
        {
            var clas = SyntaxFactory.InterfaceDeclaration(cla.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            return clas;
        }

        public static MethodDeclarationSyntax ToSyntax(this CMethod met)
        {
            var method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(met.Type), met.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block());
            return method;
        }

        public static FieldDeclarationSyntax ToSyntax(this CField fld)
        {
            var variable = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(fld.Type))
                .AddVariables(SyntaxFactory.VariableDeclarator(fld.Name));
            var field = SyntaxFactory.FieldDeclaration(variable)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
            return field;
        }

        public static PropertyDeclarationSyntax ToSyntax(this CProperty prop)
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

        public static NamespaceDeclarationSyntax ToSyntax(this CNamespace nsp)
        {
            var name = ToName(nsp.Name);
            var usings = nsp.Usings.Select(ToUsing).ToArray();
            var space = SyntaxFactory.NamespaceDeclaration(name)
                .AddUsings(usings)
                .AddMembers(nsp.Members.Select(ToSyntax).ToArray());
            return space;
        }

        public static CompilationUnitSyntax ToSyntax(this CUnit nsp)
        {
            var usings = nsp.Usings.Select(ToUsing).ToArray();
            var space = SyntaxFactory.CompilationUnit()
                .AddUsings(usings)
                .AddMembers(nsp.Members.Select(ToSyntax).ToArray());
            return space;
        }

        public static MemberDeclarationSyntax ToSyntax(this CMember member)
        {
            return member switch
            {
                CEnum e => ToSyntax(e),
                CStruct s => ToSyntax(s),
                CRecord r => ToSyntax(r),
                CDelegate d => ToSyntax(d),
                CInterface i => ToSyntax(i),
                CNamespace n => ToSyntax(n),
                CClass c => ToSyntax(c),
                CField f => ToSyntax(f),
                CProperty p => ToSyntax(p),
                CMethod m => ToSyntax(m),
                _ => throw new InvalidOperationException($"{member} ?!")
            };
        }

        public static UsingDirectiveSyntax ToUsing(string text)
        {
            var name = ToName(text);
            return SyntaxFactory.UsingDirective(name);
        }
    }
}