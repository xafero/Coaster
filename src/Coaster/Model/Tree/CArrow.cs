using System.Collections.Generic;
using Coaster.API.Tree;

namespace Coaster.Model.Tree
{
    public sealed class CArrow : IArrow
    {
        public IList<string> Statements => [Expression];

        public string Expression { get; set; }
    }
}