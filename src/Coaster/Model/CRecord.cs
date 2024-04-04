using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CRecord : CMember, IHasInterfaces
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();
    }
}