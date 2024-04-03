using System.Collections.Generic;

namespace Coaster
{
    public sealed class Namespace
    {
        public string Name { get; set; }

        public ISet<string> Usings { get; set; } = new SortedSet<string>();

        public IList<Member> Members { get; set; } = new List<Member>();
    }
}