using System.Runtime.InteropServices.ComTypes;
using Coaster.API.Top;

namespace Coaster.Model
{
    public sealed class CEnumVal : IEnumVal
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}