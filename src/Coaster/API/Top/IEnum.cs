using System.Collections.Generic;
using Coaster.API.Mod;
using Coaster.API.Part;

namespace Coaster.API.Top
{
    public interface IEnum : INamed, ITyped, IMember, IVisible
    {
        IList<IEnumVal> Values { get; }
    }
}