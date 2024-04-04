using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CMethod : CMember, IHasVisibility, IMaybeStatic, IHasParameters
    {
        public string Type { get; set; } = "void";

        public bool IsStatic { get; set; }

        public Visibility Visibility { get; set; } = Visibility.Public;

        public List<CParam> Params { get; set; } = new();

        public List<string> Statements { get; set; } = new();
    }
}