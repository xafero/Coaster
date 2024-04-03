using System.Collections.Generic;

namespace Coaster
{
    public interface IHasInterfaces
    {
        ISet<string> Interfaces { get; }
    }
}