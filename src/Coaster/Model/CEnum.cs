using Coaster.API;

namespace Coaster.Model
{
    public sealed class CEnum : CMember, IHasVisibility
    {
        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}