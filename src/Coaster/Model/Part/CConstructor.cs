using System.Collections.Generic;
using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.API.Tree;

namespace Coaster.Model.Part
{
    public sealed class CConstructor : IConstructor
    {
        public Visibility Visibility { get; set; } = Visibility.Public;

        public IList<IParam> Params { get; } = new List<IParam>();

        public IBody Body { get; set; }
    }
}