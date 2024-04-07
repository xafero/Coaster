using System.Collections.Generic;

namespace Coaster.API.Part
{
    public interface IHasParameters
    {
        IList<IParam> Params { get; }
    }
}