using System.Collections.Generic;
using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.API.Top;

namespace Coaster.Model.Top
{
    public sealed class CRecord : IRecord
    {
        public string Name { get; set; }

        public ISet<string> Interfaces { get; } = new SortedSet<string>();

        public IList<IParam> Params { get; } = new List<IParam>();

        public Visibility Visibility { get; set; } = Visibility.Public;

        public Inherit Inherit { get; set; }

        public string Base { get; set; }

        public RecMode Mode { get; set; }

        public IList<IMember> Members { get; } = new List<IMember>();

        public Modifier Modifier { get; set; }
    }
}