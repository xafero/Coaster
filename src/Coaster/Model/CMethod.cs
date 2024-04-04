using Coaster.API;

namespace Coaster.Model
{
    public sealed class CMethod : CMember, IHasVisibility, IMaybeStatic
    {
        public string Type { get; set; } = "void";

        public bool IsStatic { get; set; }

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}