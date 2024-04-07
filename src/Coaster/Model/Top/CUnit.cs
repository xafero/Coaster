using System.Collections.Generic;
using Coaster.API.Top;

namespace Coaster.Model
{
    public sealed class CUnit : IUnit
    {
        public ISet<string> Usings { get; } = new SortedSet<string>();

        public IList<IMember> Members { get; } = new List<IMember>();
    }
}