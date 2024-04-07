using System;
using System.Collections.Generic;
using System.Linq;
using Coaster.API;
using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.API.Top;
using Coaster.API.Tree;
using Coaster.Utils;
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

        public static BaseTypeSyntax[] ToBaseTypes(IEnumerable<string> inputs)
        {
            var bases = inputs.Select(ToBaseType).ToArray();
            return bases.Length == 0 ? null : bases;
        }

        public static BaseTypeSyntax[] ToBaseTypes(this IHasInterfaces cla)
            => ToBaseTypes(cla.Interfaces);

        public static BaseTypeSyntax[] ToBaseTypes<T>(this T cla) where T : IHasInterfaces, IHasBase
            => ToBaseTypes(cla.Base.NullIfEmpty().AsArray().Concat(cla.Interfaces));

        public static ClassDeclarationSyntax ToSyntax(this IClass cla)
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

        public static RecordDeclarationSyntax ToSyntax(this IRecord cla)
        {
            var rec = SyntaxFactory.Token(SyntaxKind.RecordKeyword);
            var clas = SyntaxFactory.RecordDeclaration(rec, cla.Name)
                .AddModifiers(GetModifiers(cla));
            if (cla.Mode.ToSyntax() is { } cos)
                clas = clas.WithClassOrStructKeyword(cos);
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            var hasMembers = cla.Members.Any();
            if (ToParamSyntax(cla) is { } pl)
                clas = clas.WithParameterList(pl);
            else if (!hasMembers)
                clas = clas.WithParameterList(SyntaxFactory.ParameterList());
            clas = hasMembers
                ? clas.WithOpenBraceToken(GetOpenBrace()).AddMembers(ToMemberSyntax(cla))
                    .WithCloseBraceToken(GetCloseBrace())
                : clas.WithSemicolonToken(GetSemi());
            return clas;
        }

        public static DelegateDeclarationSyntax ToSyntax(this IDelegate cla)
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

        public static EventDeclarationSyntax ToSyntax(this IEvent cla)
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

        public static SyntaxToken GetOpenBrace()
        {
            return SyntaxFactory.Token(SyntaxKind.OpenBraceToken);
        }

        public static SyntaxToken GetCloseBrace()
        {
            return SyntaxFactory.Token(SyntaxKind.CloseBraceToken);
        }

        public static StructDeclarationSyntax ToSyntax(this IStruct cla)
        {
            var clas = SyntaxFactory.StructDeclaration(cla.Name)
                .AddModifiers(GetModifiers(cla))
                .AddMembers(ToMemberSyntax(cla));
            if (ToBaseTypes(cla) is { } bases)
                clas = clas.AddBaseListTypes(bases);
            return clas;
        }

        public static SeparatedSyntaxList<BaseTypeSyntax> ToSepBaseTypes(params BaseTypeSyntax[] types)
        {
            return SyntaxFactory.SeparatedList(types);
        }

        public static BaseListSyntax ToBaseListTypes(params BaseTypeSyntax[] types)
        {
            return SyntaxFactory.BaseList(ToSepBaseTypes(types));
        }

        public static EnumDeclarationSyntax ToSyntax(this IEnum enu)
        {
            var ed = SyntaxFactory.EnumDeclaration(enu.Name)
                .AddModifiers(GetModifiers(enu))
                .WithMembers(ToSyntax(enu.Values));
            if (enu.Type is { Length: >= 1 } enumType)
                ed = ed.WithBaseList(ToBaseListTypes(ToBaseType(enumType)));
            return ed;
        }

        public static SeparatedSyntaxList<EnumMemberDeclarationSyntax> ToSyntax(IList<IEnumVal> values)
        {
            var items = values.Select(v =>
            {
                var end = SyntaxFactory.EnumMemberDeclaration(v.Name);
                if (v.Value is { Length: >= 1 } vv)
                {
                    var vve = SyntaxFactory.ParseExpression(vv);
                    end = end.WithEqualsValue(SyntaxFactory.EqualsValueClause(vve));
                }
                return end;
            });
            var list = SyntaxFactory.SeparatedList(items);
            return list;
        }

        public static InterfaceDeclarationSyntax ToSyntax(this IInterface cla)
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

        public static SyntaxToken? ToSyntax(this Inherit inh)
        {
            switch (inh)
            {
                case Inherit.None: return null;
                case Inherit.Virtual: return SyntaxFactory.Token(SyntaxKind.VirtualKeyword);
                case Inherit.Override: return SyntaxFactory.Token(SyntaxKind.OverrideKeyword);
                case Inherit.Abstract: return SyntaxFactory.Token(SyntaxKind.AbstractKeyword);
                case Inherit.Sealed: return SyntaxFactory.Token(SyntaxKind.SealedKeyword);
                default: throw new ArgumentOutOfRangeException(nameof(inh), inh, null);
            }
        }

        public static SyntaxToken? ToSyntax(this RecMode rm)
        {
            switch (rm)
            {
                case RecMode.None: return null;
                case RecMode.Struct: return SyntaxFactory.Token(SyntaxKind.StructKeyword);
                case RecMode.Class: return SyntaxFactory.Token(SyntaxKind.ClassKeyword);
                default: throw new ArgumentOutOfRangeException(nameof(rm), rm, null);
            }
        }

        public static SyntaxToken[] GetModifiers(object obj, params SyntaxToken?[] adds)
        {
            var list = new List<SyntaxToken>();
            if (obj is IVisible hv && ToSyntax(hv.Visibility) is { } hvv)
                list.Add(hvv);
            if (obj is IModified im)
            {
                if (im.Modifier.HasFlag(Modifier.Static))
                    list.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
                if (im.Modifier.HasFlag(Modifier.Readonly))
                    list.Add(SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));
            }
            if (obj is IInherited ih && ToSyntax(ih.Inherit) is { } ihv)
                list.Add(ihv);
            if (adds?.Where(a => a != default).Cast<SyntaxToken>().ToArray() is { Length: >= 1 } al)
                list.AddRange(al);
            return list.ToArray();
        }

        public static ParameterSyntax ToSyntax(this IParam par)
        {
            var ipn = SyntaxFactory.Identifier(par.Name);
            var prm = SyntaxFactory.Parameter(ipn)
                .WithType(SyntaxFactory.ParseTypeName(par.Type));
            if (par.Value.NullIfEmpty() is { } paramVal)
            {
                var defaultExpr = SyntaxFactory.ParseExpression(paramVal);
                prm = prm.WithDefault(SyntaxFactory.EqualsValueClause(defaultExpr));
            }
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

        public static ArgumentListSyntax ToArgSyntax(this IEnumerable<string> args)
        {
            var list = SyntaxFactory.ParseArgumentList($"({string.Join(", ", args)})");
            return list;
        }

        public static ConstructorInitializerSyntax ToSyntax(this IInitializer ini)
        {
            var kind = ini.IsThis ? SyntaxKind.ThisConstructorInitializer : SyntaxKind.BaseConstructorInitializer;
            var init = SyntaxFactory.ConstructorInitializer(kind);
            if (ini.Args is { Count: >= 1 })
                init = init.WithArgumentList(ToArgSyntax(ini.Args));
            return init;
        }

        public static ConstructorDeclarationSyntax ToSyntax(this IConstructor met, IHasMembers owner)
        {
            var name = (owner as INamed)!.Name;
            var method = SyntaxFactory.ConstructorDeclaration(name)
                .AddModifiers(GetModifiers(met));
            if (ToArrowSyntax(met) is { } arrow)
                method = method.WithExpressionBody(arrow).WithSemicolonToken(GetSemi());
            else if (ToBlockSyntax(met) is { } block)
                method = method.WithBody(block);
            if (ToParamSyntax(met) is { } pl)
                method = method.WithParameterList(pl);
            if (met.Init is { } init)
                method = method.WithInitializer(init.ToSyntax());
            return method;
        }

        public static MethodDeclarationSyntax ToSyntax(this IMethod met, IHasMembers owner)
        {
            met.Apply(owner);

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

        public static bool IsArrow(this IBody body)
        {
            return body is IArrow;
        }

        public static ArrowExpressionClauseSyntax ToArrowSyntax(this IHasBody owner)
        {
            if (owner?.Body == null || !owner.Body.IsArrow())
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
            if (owner?.Body == null || owner.Body.IsArrow())
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

        public static FieldDeclarationSyntax ToSyntax(this IField fld)
        {
            var variable = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(fld.Type))
                .AddVariables(SyntaxFactory.VariableDeclarator(fld.Name));
            var field = SyntaxFactory.FieldDeclaration(variable)
                .AddModifiers(GetModifiers(fld));
            return field;
        }

        public static bool IsInterface(this IHasMembers obj)
        {
            return obj is IInterface;
        }

        public static bool IsRecord(this IHasMembers obj)
        {
            return obj is IRecord;
        }

        public static List<AccessorDeclarationSyntax> ToAccessSyntax(this IProperty prop)
        {
            var get = SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var init = SyntaxFactory.AccessorDeclaration(SyntaxKind.InitAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var set = SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            var ala = new List<AccessorDeclarationSyntax>();
            if (prop.Mode is PropMode.Get or PropMode.GetSet or PropMode.GetInit) ala.Add(get);
            if (prop.Mode is PropMode.GetInit) ala.Add(init);
            if (prop.Mode is PropMode.Set or PropMode.GetSet) ala.Add(set);
            return ala;
        }

        public static PropertyDeclarationSyntax ToSyntax(this IProperty prop, IHasMembers owner)
        {
            prop.Apply(owner);

            var ala = prop.ToAccessSyntax();

            var pt = SyntaxFactory.ParseTypeName(prop.Type);
            var property = SyntaxFactory.PropertyDeclaration(pt, prop.Name)
                .AddModifiers(GetModifiers(prop))
                .AddAccessorListAccessors(ala.ToArray());
            return property;
        }

        public static NamespaceDeclarationSyntax ToSyntax(this INamespace nsp)
        {
            var name = ToName(nsp.Name);
            var usings = nsp.Usings.Select(ToUsing).ToArray();
            var space = SyntaxFactory.NamespaceDeclaration(name)
                .AddUsings(usings)
                .AddMembers(ToMemberSyntax(nsp));
            return space;
        }

        public static CompilationUnitSyntax ToSyntax(this IUnit nsp)
        {
            var usings = nsp.Usings.Select(ToUsing).ToArray();
            var space = SyntaxFactory.CompilationUnit()
                .AddUsings(usings)
                .AddMembers(ToMemberSyntax(nsp));
            return space;
        }

        public static MemberDeclarationSyntax ToSyntax(this IMember member, IHasMembers owner)
        {
            return member switch
            {
                IEnum e => ToSyntax(e),
                IStruct s => ToSyntax(s),
                IRecord r => ToSyntax(r),
                IDelegate d => ToSyntax(d),
                IInterface i => ToSyntax(i),
                INamespace n => ToSyntax(n),
                IClass c => ToSyntax(c),
                IField f => ToSyntax(f),
                IEvent v => ToSyntax(v),
                IProperty p => ToSyntax(p, owner),
                IConstructor o => ToSyntax(o, owner),
                IMethod m => ToSyntax(m, owner),
                _ => throw new InvalidOperationException($"{member} ?!")
            };
        }

        public static UsingDirectiveSyntax ToUsing(string text)
        {
            var name = ToName(text);
            return SyntaxFactory.UsingDirective(name);
        }

        public static string ToText(this IUnit unit)
        {
            var syntax = unit.ToSyntax();
            return syntax.ToText();
        }
    }
}