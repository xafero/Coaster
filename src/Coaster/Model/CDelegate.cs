using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CDelegate : CMember, IHasVisibility, IHasParameters
    {
        public string Type { get; set; } = "void";

        public Visibility Visibility { get; set; } = Visibility.Public;

        public List<CParam> Params { get; set; } = new();
    }
}