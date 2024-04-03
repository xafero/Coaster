using System.Collections.Generic;

namespace Coaster
{
    public sealed class CNamespace
    {
        public string Name { get; set; }

        public ISet<string> Usings { get; set; } = new SortedSet<string>();

        public IList<CMember> Members { get; set; } = new List<CMember>();
    }
}