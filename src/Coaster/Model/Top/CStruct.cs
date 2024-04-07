using System.Collections.Generic;
using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.API.Top;

namespace Coaster.Model.Top
{
    public sealed class CStruct : IStruct
    {
        public string Name { get; set; }

        public IList<IMember> Members { get; } = new List<IMember>();

        public ISet<string> Interfaces { get; } = new SortedSet<string>();

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}