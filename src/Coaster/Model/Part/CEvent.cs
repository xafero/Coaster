using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.Utils;

namespace Coaster.Model.Part
{
    public sealed class CEvent : IEvent
    {
        public string Name { get; set; }

        public string Type { get; set; } = Defaults.EventHandler;

        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}