using Coaster.API.Mod;
using Coaster.API.Part;

namespace Coaster.Model.Part
{
    public sealed class CField : IField
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Visibility Visibility { get; set; } = Visibility.Private;
    }
}