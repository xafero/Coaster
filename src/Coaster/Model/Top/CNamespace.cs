using System.Collections.Generic;
using Coaster.API.Part;
using Coaster.API.Top;

namespace Coaster.Model.Top
{
    public sealed class CNamespace : INamespace
    {
        public string Name { get; set; }

        public ISet<string> Usings { get; } = new SortedSet<string>();

        public IList<IMember> Members { get; } = new List<IMember>();
    }
}