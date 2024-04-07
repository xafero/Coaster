using System.Collections.Generic;

namespace Coaster.API.Top
{
    public interface IHasMembers
    {
        IList<IMember> Members { get; }
    }
}