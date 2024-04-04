using Coaster.API;

namespace Coaster.Model
{
    public sealed class CField : CMember, IHasVisibility
    {
        public string Type { get; set; }

        public Visibility Visibility { get; set; } = Visibility.Private;
    }
}