using System.Collections.Generic;

namespace Coaster.API
{
    public interface IUsed
    {
        ISet<string> Usings { get; }
    }
}