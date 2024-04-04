using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CUnit : IHasMembers
    {
        public ISet<string> Usings { get; set; } = new SortedSet<string>();

        public IList<CMember> Members { get; set; } = new List<CMember>();
    }
}