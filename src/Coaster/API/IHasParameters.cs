using System.Collections.Generic;
using Coaster.Model;

namespace Coaster.API
{
    public interface IHasParameters
    {
        List<CParam> Params { get; }
    }
}