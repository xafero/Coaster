using System.Collections.Generic;
using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.API.Top;

namespace Coaster.Model.Top
{
    public sealed class CEnum : IEnum
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public IList<IEnumVal> Values { get; } = new List<IEnumVal>();

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}