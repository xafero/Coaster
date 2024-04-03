using System.Collections.Generic;

namespace Coaster
{
    public sealed class CInterface : CMember, IHasInterfaces
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();
    }
}