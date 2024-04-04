using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CInterface : CMember, IHasInterfaces
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();
    }
}