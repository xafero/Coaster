using System.Collections.Generic;

namespace Coaster.API.Top
{
    public interface IEnum : INamed, ITyped, IMember
    {
        IList<IEnumVal> Values { get; }
    }
}