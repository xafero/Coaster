using System.Collections.Generic;

namespace Coaster
{
    public sealed class Class : Member
    {
        public string Name { get; set; }

        public ISet<string> Interfaces { get; set; } = new HashSet<string>();
    }
}