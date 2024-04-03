using System.Collections.Generic;

namespace Coaster
{
    public sealed class CUnit
    {
        public ISet<string> Usings { get; set; } = new SortedSet<string>();

        public IList<CMember> Members { get; set; } = new List<CMember>();
    }
}