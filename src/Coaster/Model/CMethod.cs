using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CMethod : CMember, IHasVisibility, IMaybeStatic, IMaybeOverride, IHasParameters, IHasBody
    {
        public string Type { get; set; } = "void";

        public bool IsStatic { get; set; }

        public bool IsOverride { get; set; }

        public Visibility Visibility { get; set; } = Visibility.Public;

        public List<CParam> Params { get; set; } = new();

        public bool IsConstructor { get; set; }

        public CBody Body { get; set; } = new();
    }
}