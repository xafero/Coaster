using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CEnum : CMember, IHasVisibility
    {
        public string Type { get; set; }

        public Visibility Visibility { get; set; } = Visibility.Public;

        public IList<CEnumVal> Values { get; set; } = new List<CEnumVal>();
    }
}