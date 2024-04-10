using Coaster.API.Mod;
using Coaster.API.Part;

namespace Coaster.Model.Part
{
    public sealed class CParam : IParam
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public ParamMod Mod { get; set; }
    }
}