using System.Collections.Generic;

namespace Coaster
{
    public sealed class CRecord : CMember, IHasInterfaces
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();
    }
}