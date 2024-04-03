using System.Collections.Generic;

namespace Coaster
{
    public sealed class Class : Member
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();
    }
}