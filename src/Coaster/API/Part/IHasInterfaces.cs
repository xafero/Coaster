using System.Collections.Generic;

namespace Coaster.API.Part
{
    public interface IHasInterfaces
    {
        ISet<string> Interfaces { get; }
    }
}