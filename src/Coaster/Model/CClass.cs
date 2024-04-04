using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CClass : CMember, IHasInterfaces, IHasVisibility, IMaybeStatic
    {
        public CClass()
        {
            Name = "Sample";
        }

        public ISet<string> Interfaces { get; set; } = new HashSet<string>();

        public IList<CMember> Members { get; set; } = new List<CMember>();

        public bool IsStatic { get; set; }

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}