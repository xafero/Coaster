using System.Collections.Generic;
using Coaster.API.Tree;

namespace Coaster.Model.Tree
{
    public sealed class CBody : IBody
    {
        public IList<string> Statements { get; } = new List<string>();
    }
}