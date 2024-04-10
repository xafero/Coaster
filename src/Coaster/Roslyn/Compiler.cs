using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

#if NET6_0_OR_GREATER
using Microsoft.CSharp.RuntimeBinder;
#endif

namespace Coaster.Roslyn
{
    public static class Compiler
    {
        private static PortableExecutableReference CreateMetaRef(Type type, string @override = null)
        {
            var ass = type.Assembly;
            var loc = ass.Location;
            if (@override != null)
            {
                var dir = Path.GetDirectoryName(loc)!;
                loc = Path.Combine(dir, $"{@override}.dll");
            }
            return MetadataReference.CreateFromFile(loc);
        }

        private static MetadataReference[] GetReferences()
        {
            return
            [
#if NET6_0_OR_GREATER
                CreateMetaRef(typeof(CSharpArgumentInfo)),
#endif
                CreateMetaRef(typeof(LinkedList<string>)),
                CreateMetaRef(typeof(Console)),
                CreateMetaRef(typeof(Enumerable)),
                CreateMetaRef(typeof(ExpandoObject)),
                CreateMetaRef(typeof(object)),
                CreateMetaRef(typeof(AppDomain), "System.Runtime")
            ];
        }

        public static T CreateDelegate<T>(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);
            var trees = new[] { tree };
            return CreateDelegate<T>(trees);
        }

        public static T CreateDelegate<T>(params SyntaxTree[] trees)
        {
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var references = GetReferences();
            var assemblyName = $"Dyn_{Guid.NewGuid().ToString("N").Substring(0, 8)}";
            var compilation = CSharpCompilation.Create(assemblyName, syntaxTrees: trees,
                references: references, options: options);
            using var mem = new MemoryStream();
            var result = compilation.Emit(mem);
            if (!result.Success)
            {
                var debug = result.Diagnostics.Select(d => d.ToString());
                throw new InvalidOperationException(string.Join("\n", debug));
            }
            mem.Seek(0, SeekOrigin.Begin);
            var dynDll = Assembly.Load(mem.ToArray());
            var dynType = dynDll.GetTypes().First();
            var dynMethod = dynType.GetMethods().First();
            var dynInst = Activator.CreateInstance(dynType);
            var rawDel = Delegate.CreateDelegate(typeof(T), dynInst, dynMethod);
            var dynDelegate = (T)(object)rawDel;
            return dynDelegate;
        }
    }
}