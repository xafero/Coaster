using System.Collections.Generic;
using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.API.Tree;
using Coaster.Model.Tree;
using Coaster.Roslyn;
using Coaster.Utils;

namespace Coaster.Model.Part
{
    public sealed class CMethod : IMethod
    {
        public string Name { get; set; }

        public string Type { get; set; } = Defaults.Void;

        public Visibility Visibility { get; set; }

        public Modifier Modifier { get; set; }

        public Inherit Inherit { get; set; }

        public IList<IParam> Params { get; } = new List<IParam>();

        public IBody Body { get; set; }

        public void Apply(IHasMembers owner)
        {
            if (owner.IsInterface())
            {
                return;
            }
            Body ??= new CBody();
            Visibility = Visibility.IfZero(Visibility.Public);
        }
    }
}