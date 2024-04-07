using System.Collections.Generic;

namespace Coaster.API.Top
{
    public interface IHasParameters
    {
        IList<IParam> Params { get; }
    }
}