using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CInterface : CMember, IHasInterfaces, IHasVisibility
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}