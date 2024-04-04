using Coaster.API;

namespace Coaster.Model
{
    public sealed class CDelegate : CMember, IHasVisibility
    {
        public string Type { get; set; } = "void";

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}