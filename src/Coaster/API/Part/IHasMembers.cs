using System.Collections.Generic;

namespace Coaster.API.Part
{
    public interface IHasMembers
    {
        IList<IMember> Members { get; }
    }
}