using System.Collections.Generic;

namespace Coaster.API
{
    public interface IHasInterfaces
    {
        ISet<string> Interfaces { get; }
    }
}