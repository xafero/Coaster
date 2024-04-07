using System.Collections.Generic;
using Coaster.API.Top;

namespace Coaster.Model
{
    public sealed class CClass : IClass, IMember
    {
        public string Name { get; set; }

        public IList<IMember> Members { get; } = new List<IMember>();

        public ISet<string> Interfaces { get; } = new SortedSet<string>();
    }
}