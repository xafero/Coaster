using System.Collections.Generic;

namespace Coaster.API.Tree
{
    public interface IBody
    {
        IList<string> Statements { get; }
    }
}