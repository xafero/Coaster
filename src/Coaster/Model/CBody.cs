using System.Collections.Generic;

namespace Coaster.Model
{
    public sealed class CBody
    {
        public List<string> Statements { get; set; } = new();

        public bool IsArrow { get; set; }
    }
}