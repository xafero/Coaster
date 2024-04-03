using System.Collections.Generic;

namespace Coaster
{
    public sealed class CStruct : CMember, IHasInterfaces
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();

        public IList<CMember> Members { get; set; } = new List<CMember>();
    }
}