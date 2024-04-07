using System.Collections.Generic;

namespace Coaster.API.Tree
{
    public interface IInitializer
    {
        bool IsThis { get; }

        IList<string> Args { get; }
    }
}