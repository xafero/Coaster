using System.Collections.Generic;
using Coaster.Model;

namespace Coaster.API
{
    public interface IHasMembers
    {
        IList<CMember> Members { get; }
    }
}