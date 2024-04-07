using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.Roslyn;

namespace Coaster.Model.Part
{
    public sealed class CProperty : IProperty
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Visibility Visibility { get; set; }

        public PropMode Mode { get; set; } = PropMode.GetSet;

        public void Apply(IHasMembers owner)
        {
            if (owner.IsInterface())
            {
                return;
            }
            Visibility = Visibility.Public;
        }
    }
}