using System.Collections.Generic;

namespace Coaster
{
    public sealed class Class : Member
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();

        public IList<Member> Members { get; set; } = new List<Member>();
    }
}