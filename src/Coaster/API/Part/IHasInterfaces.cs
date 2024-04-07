using System.Collections.Generic;

namespace Coaster.API.Top
{
    public interface IHasInterfaces
    {
        ISet<string> Interfaces { get; }
    }
}