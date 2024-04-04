using System;
using System.Collections.Generic;
using System.Linq;
using Coaster.API;
using Coaster.Model;
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
                .AddModifiers(GetModifiers(cla))
                .AddMembers(cla.Members.Select(ToSyntax).ToArray());
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            return clas;
        }

        public static RecordDeclarationSyntax ToSyntax(this CRecord cla)
        {
            var rec = SyntaxFactory.Token(SyntaxKind.RecordKeyword);
            var clas = SyntaxFactory.RecordDeclaration(rec, cla.Name)
                .AddModifiers(GetModifiers(cla));
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
                .AddModifiers(GetModifiers(cla));
            clas = clas.WithParameterList(SyntaxFactory.ParameterList());
            clas = clas.WithSemicolonToken(GetSemi());
            return clas;
        }

        public static EventDeclarationSyntax ToSyntax(this CEvent cla)
        {
            var rt = SyntaxFactory.ParseTypeName(cla.Type);
            var clas = SyntaxFactory.EventDeclaration(rt, cla.Name)
                .AddModifiers(GetModifiers(cla));
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
                .AddModifiers(GetModifiers(cla))
                .AddMembers(cla.Members.Select(ToSyntax).ToArray());
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            return clas;
        }

        public static EnumDeclarationSyntax ToSyntax(this CEnum enu)
        {
            var ed = SyntaxFactory.EnumDeclaration(enu.Name)
                .AddModifiers(GetModifiers(enu));
            return ed;
        }

        public static InterfaceDeclarationSyntax ToSyntax(this CInterface cla)
        {
            var clas = SyntaxFactory.InterfaceDeclaration(cla.Name)
                .AddModifiers(GetModifiers(cla));
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            return clas;
        }

        public static SyntaxToken? ToSyntax(this Visibility vis)
        {
            switch (vis)
            {
                case Visibility.None: return null;
                case Visibility.Private: return SyntaxFactory.Token(SyntaxKind.PrivateKeyword);
                case Visibility.Protected: return SyntaxFactory.Token(SyntaxKind.ProtectedKeyword);
                case Visibility.Internal: return SyntaxFactory.Token(SyntaxKind.InternalKeyword);
                case Visibility.Public: return SyntaxFactory.Token(SyntaxKind.PublicKeyword);
                default: throw new ArgumentOutOfRangeException(nameof(vis), vis, null);
            }
        }

        public static SyntaxToken[] GetModifiers(object obj)
        {
            var list = new List<SyntaxToken>();
            if (obj is IHasVisibility hv && ToSyntax(hv.Visibility) is { } hvv)
                list.Add(hvv);
            if (obj is IMaybeStatic { IsStatic: true })
                list.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            return list.ToArray();
        }

        public static ParameterSyntax ToSyntax(this CParam par)
        {
            var ipn = SyntaxFactory.Identifier(par.Name);
            var prm = SyntaxFactory.Parameter(ipn)
                .WithType(SyntaxFactory.ParseTypeName(par.Type));
            return prm;
        }

        public static ParameterListSyntax ToParamSyntax(IHasParameters hp)
        {
            if (hp.Params.Count == 0)
                return null;
            var args = hp.Params.Select(p => p.ToSyntax());
            var pas = SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(args));
            return pas;
        }

        public static MethodDeclarationSyntax ToSyntax(this CMethod met)
        {
            var method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(met.Type), met.Name)
                .AddModifiers(GetModifiers(met))
                .WithBody(ToBlockSyntax(met.Statements));
            if (ToParamSyntax(met) is { } pl)
                method = method.WithParameterList(pl);
            return method;
        }

        public static BlockSyntax ToBlockSyntax(IEnumerable<string> statements)
        {
            var lines = statements.Select(s =>
            {
                var text = s.EndsWith(";") ? s : $"{s};";
                return SyntaxFactory.ParseStatement(text);
            });
            var block = SyntaxFactory.Block(lines);
            return block;
        }

        public static FieldDeclarationSyntax ToSyntax(this CField fld)
        {
            var variable = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(fld.Type))
                .AddVariables(SyntaxFactory.VariableDeclarator(fld.Name));
            var field = SyntaxFactory.FieldDeclaration(variable)
                .AddModifiers(GetModifiers(fld));
            return field;
        }

        public static PropertyDeclarationSyntax ToSyntax(this CProperty prop)
        {
            var get = SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var set = SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var property = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(prop.Type), prop.Name)
                .AddModifiers(GetModifiers(prop))
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
                CEvent v => ToSyntax(v),
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

        public static string ToText(this CUnit unit)
        {
            var syntax = unit.ToSyntax();
            return syntax.ToText();
        }
    }
}