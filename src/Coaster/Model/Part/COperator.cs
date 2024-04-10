using System.Collections.Generic;
using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.API.Tree;
using Coaster.Utils;

namespace Coaster.Model.Part
{
    public sealed class COperator : IOperator
    {
        public IBody Body { get; set; }

        public string Type { get; set; } = Defaults.Bool;

        public OpMode Kind { get; set; }

        public IList<IParam> Params { get; } = new List<IParam>();

        public Visibility Visibility => Visibility.Public;
        public Modifier Modifier => Modifier.Static;
    }
}