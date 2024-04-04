using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CStruct : CMember, IHasInterfaces, IHasVisibility, IHasMembers
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();

        public IList<CMember> Members { get; set; } = new List<CMember>();

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}