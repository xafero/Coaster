using System.Collections.Generic;
using Coaster.API.Tree;

namespace Coaster.Model.Tree
{
    public sealed class CInit : IInitializer
    {
        public bool IsThis { get; set; }

        public IList<string> Args { get; } = new List<string>();
    }
}