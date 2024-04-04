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
                .AddMembers(ToMemberSyntax(cla));
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            return clas;
        }

        public static MemberDeclarationSyntax[] ToMemberSyntax(IHasMembers owner)
        {
            return owner.Members.Select(m => ToSyntax(m, owner)).ToArray();
        }

        public static RecordDeclarationSyntax ToSyntax(this CRecord cla)
        {
            var rec = SyntaxFactory.Token(SyntaxKind.RecordKeyword);
            var clas = SyntaxFactory.RecordDeclaration(rec, cla.Name)
                .AddModifiers(GetModifiers(cla));
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            if (ToParamSyntax(cla) is { } pl)
                clas = clas.WithParameterList(pl);
            else
                clas = clas.WithParameterList(SyntaxFactory.ParameterList());
            clas = clas.WithSemicolonToken(GetSemi());
            return clas;
        }

        public static DelegateDeclarationSyntax ToSyntax(this CDelegate cla)
        {
            var rt = SyntaxFactory.ParseTypeName(cla.Type);
            var clas = SyntaxFactory.DelegateDeclaration(rt, cla.Name)
                .AddModifiers(GetModifiers(cla));
            if (ToParamSyntax(cla) is { } pl)
                clas = clas.WithParameterList(pl);
            else
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
                .AddMembers(ToMemberSyntax(cla));
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
                .AddModifiers(GetModifiers(cla))
                .AddMembers(ToMemberSyntax(cla));
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
            if (obj is IMaybeOverride { IsOverride: true })
                list.Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));
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

        public static ConstructorDeclarationSyntax ToCtrSyntax(this CMethod met, IHasMembers owner)
        {
            var name = met.Name ?? (owner as IHasName)!.Name;
            var method = SyntaxFactory.ConstructorDeclaration(name)
                .AddModifiers(GetModifiers(met));
            if (ToArrowSyntax(met) is { } arrow)
                method = method.WithExpressionBody(arrow).WithSemicolonToken(GetSemi());
            else if (ToBlockSyntax(met) is { } block)
                method = method.WithBody(block);
            if (ToParamSyntax(met) is { } pl)
                method = method.WithParameterList(pl);
            return method;
        }

        public static BaseMethodDeclarationSyntax ToSyntax(this CMethod met, IHasMembers owner)
        {
            if (met.IsConstructor)
            {
                return ToCtrSyntax(met, owner);
            }
            if (owner.IsInterface())
            {
                met.Body = null;
                met.Visibility = Visibility.None;
            }
            var method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(met.Type), met.Name)
                .AddModifiers(GetModifiers(met));
            if (ToArrowSyntax(met) is { } arrow)
                method = method.WithExpressionBody(arrow).WithSemicolonToken(GetSemi());
            else if (ToBlockSyntax(met) is { } block)
                method = method.WithBody(block);
            else
                method = method.WithSemicolonToken(GetSemi());
            if (ToParamSyntax(met) is { } pl)
                method = method.WithParameterList(pl);
            return method;
        }

        public static ArrowExpressionClauseSyntax ToArrowSyntax(this IHasBody owner)
        {
            if (owner?.Body is not { IsArrow: true })
            {
                return null;
            }
            var statements = owner.Body.Statements;
            var single = SyntaxFactory.ParseExpression(statements.Single());
            var arrow = SyntaxFactory.ArrowExpressionClause(single);
            return arrow;
        }

        public static BlockSyntax ToBlockSyntax(this IHasBody owner)
        {
            if (owner?.Body == null || owner.Body.IsArrow)
            {
                return null;
            }
            var statements = owner.Body.Statements;
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

        public static bool IsInterface(this IHasMembers obj)
        {
            return obj is CInterface;
        }

        public static PropertyDeclarationSyntax ToSyntax(this CProperty prop, IHasMembers owner)
        {
            if (owner.IsInterface()) prop.Visibility = Visibility.None;

            var get = SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var set = SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var ala = new List<AccessorDeclarationSyntax>();
            if (prop.Mode is PropMode.Get or PropMode.GetSet) ala.Add(get);
            if (prop.Mode is PropMode.Set or PropMode.GetSet) ala.Add(set);

            var pt = SyntaxFactory.ParseTypeName(prop.Type);
            var property = SyntaxFactory.PropertyDeclaration(pt, prop.Name)
                .AddModifiers(GetModifiers(prop))
                .AddAccessorListAccessors(ala.ToArray());
            return property;
        }

        public static NamespaceDeclarationSyntax ToSyntax(this CNamespace nsp)
        {
            var name = ToName(nsp.Name);
            var usings = nsp.Usings.Select(ToUsing).ToArray();
            var space = SyntaxFactory.NamespaceDeclaration(name)
                .AddUsings(usings)
                .AddMembers(ToMemberSyntax(nsp));
            return space;
        }

        public static CompilationUnitSyntax ToSyntax(this CUnit nsp)
        {
            var usings = nsp.Usings.Select(ToUsing).ToArray();
            var space = SyntaxFactory.CompilationUnit()
                .AddUsings(usings)
                .AddMembers(ToMemberSyntax(nsp));
            return space;
        }

        public static MemberDeclarationSyntax ToSyntax(this CMember member, IHasMembers owner)
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
                CProperty p => ToSyntax(p, owner),
                CMethod m => ToSyntax(m, owner),
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