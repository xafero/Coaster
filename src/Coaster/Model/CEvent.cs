using Coaster.API;

namespace Coaster.Model
{
    public sealed class CEvent : CMember, IHasVisibility
    {
        public string Type { get; set; } = "EventHandler";

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}