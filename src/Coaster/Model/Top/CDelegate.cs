using System.Collections.Generic;
using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.API.Top;
using Coaster.Utils;

namespace Coaster.Model.Top
{
    public sealed class CDelegate : IDelegate
    {
        public string Name { get; set; }

        public string Type { get; set; } = Defaults.Void;

        public IList<IParam> Params { get; } = new List<IParam>();

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}