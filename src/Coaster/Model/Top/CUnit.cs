﻿using System.Collections.Generic;
using Coaster.API.Part;
using Coaster.API.Top;

namespace Coaster.Model.Top
{
    public sealed class CUnit : IUnit
    {
        public ISet<string> Usings { get; } = new SortedSet<string>();

        public IList<IMember> Members { get; } = new List<IMember>();
    }
}