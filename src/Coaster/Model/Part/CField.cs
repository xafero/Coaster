using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.Utils;

namespace Coaster.Model.Part
{
    public sealed class CField : IField
    {
        public string Name { get; set; }

        public string Type { get; set; } = Defaults.Object;

        public Visibility Visibility { get; set; } = Visibility.Private;

        public string Value { get; set; }
    }
}