using System.Collections.Generic;

namespace Coaster.Model
{
    public sealed class CNamespace : CMember
    {
        public ISet<string> Usings { get; set; } = new SortedSet<string>();

        public IList<CMember> Members { get; set; } = new List<CMember>();
    }
}