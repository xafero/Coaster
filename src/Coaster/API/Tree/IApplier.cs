using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coaster.API.Top;

namespace Coaster.API.Tree
{
    public interface IApplier
    {
        void Apply(IHasMembers owner);
    }
}